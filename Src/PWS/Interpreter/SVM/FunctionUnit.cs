using Godot;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    /// <summary>
    /// This class is used to run the function, and get the return value.
    /// </summary>
    public class FunctionUnit
    {
        private BaseVariableDictionary local_variable_dictionary = new BaseVariableDictionary();
        private string pipeline_context = "null";
        private PWSGlobalVariable global_variable;
        private PWSFunctionDictionary function_dictionary;
        private PWSFunctionInformation information;
        private List<string> param_value;
        public FunctionUnit(PWSFunctionDictionary function_dictionary, PWSGlobalVariable global_variable, PWSFunctionInformation information, List<string> param_value)
        {
            this.function_dictionary = function_dictionary;
            this.global_variable = global_variable;
            this.information = information;
            this.param_value = param_value;
            // add param value
            addParam(information, param_value);
            
            // run instruction cycle
            InstructionCycle(information, param_value);
            
        }
        /// <summary>
        /// get param value, and add to local variable dictionary.
        /// </summary>
        /// <param name="information"></param>
        /// <param name="param_value"></param>
        private void addParam(PWSFunctionInformation information, List<string> param_value)
        {
            if (information.parament_name.Count != param_value.Count)
            {
                throw new($"Building Error In {information.name}: Error function param");
            }
            int i = 0;
            foreach(string param_name in information.parament_name)
            {
                local_variable_dictionary.addVariableFromString(param_name, param_value[i]);
                i++;
            }
        }
        private void InstructionCycle(PWSFunctionInformation information, List<string> param_value)
        {
            int pc = 0;
            string token_return = "";
            Stack<bool> if_stack = new Stack<bool>();
            do 
            {
                GD.Print($"PC: {pc}, Token: {information.body[pc].ToString()}, Type: {information.body[pc].type}, Pipeline: {pipeline_context}");
                
                if (stackIsSkip(if_stack))
                {
                    switch (information.body[pc].type)
                    {
                        case PWSTokenType.Assign:
                            token_return = doAssign(information.body[pc].context[2], information.body[pc].context[0]);
                            break;
                        case PWSTokenType.ForceAssign:
                            token_return = doForceAssign(information.body[pc].context[0], information.body[pc].context[2]);
                            break;
                        case PWSTokenType.Calculate:
                            token_return = doALU(information.body[pc].context[0], information.body[pc].context[1], information.body[pc].context[2]);
                            break;
                        case PWSTokenType.CallFunc:
                            token_return = doCallFunction(information.body[pc], token_return);
                            break;      
                    }
                    // mean pipeline, send context
                    if (information.body[pc].context[information.body[pc].context.Count-1] == "|")
                        pipeline_context = token_return;
                    // When meet `if`, start the if
                    if (information.body[pc].context[information.body[pc].context.Count-1] == "?")
                    {
                        if_stack.Push(token_return == "true");
                    }
                    // when meet `else`
                    if (information.body[pc].context[information.body[pc].context.Count-1] == ":")
                    {
                        if (if_stack.Count > 0)
                        {
                            bool current = if_stack.Pop(); 
                            if_stack.Push(!current);      
                        }
                    }
                }
                else
                {
                    // there code mean in the ignore statement
                    // mean end of ignore
                    if (information.body[pc].context[information.body[pc].context.Count-1] == ":")
                    {
                        if (if_stack.Count > 0)
                        {
                            bool current = if_stack.Pop(); 
                            if_stack.Push(!current);      
                        }
                    }
                    // mean end if 
                    if (information.body[pc].context[information.body[pc].context.Count-1] == ";;")
                    {
                        if_stack.Pop();
                    }
                }
                
                pc++;
            }
            while (information.body[pc].context.Last() != "#;");
        }
        private bool stackIsSkip(Stack<bool> if_stack)
        {
            return if_stack.Count == 0 || if_stack.Peek() == true;
        }
        /// <summary>
        /// Do assign, if the value is get value.
        /// Get the value from local variable dictionary or global variable dictionary.
        /// And then assign to local variable dictionary or global variable dictionary.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value_string"></param>
        private string doAssign(string name, string value_string)
        {
            Type type = AnalysesType.analysesValueGetType(value_string);
            if (type == typeof(PWSGetValue))
            {
                if (name[0] != '_')
                {
                    local_variable_dictionary.trySetDataFromList(name, value_string);
                }
                else
                {
                    global_variable.variable_dictionary.trySetDataFromList(name, value_string);
                }
            }
            if (type == typeof(PWSWildcard))
            {
                string add_value = pipeline_context;
                local_variable_dictionary.addVariableFromString(name, add_value);
                return add_value;
            } 
            local_variable_dictionary.addVariableFromString(name, value_string);
            return value_string;
        }
        private string doForceAssign(string left_string, string right_string)
        {
            left_string = changeVariableAndWildcardAsValue(left_string);
            right_string = changeVariableAndWildcardAsValue(right_string);
            return AnalysesType.forceChangeType(left_string, right_string);
        }
        /// <summary>
        /// This function is used to do ALU operation like CPU, and return the value.
        /// </summary>
        /// <param name="left_string"></param>
        /// <param name="operator_string"></param>
        /// <param name="right_string"></param>
        /// <returns></returns>
        private string doALU(string left_string, string operator_string, string right_string)
        {
            left_string = changeVariableAndWildcardAsValue(left_string);
            right_string = changeVariableAndWildcardAsValue(right_string);
            string calculate_value = ALU.calculate(left_string, operator_string, right_string);
            return calculate_value;
        }
        private string doCallFunction(PWSToken token, string token_return)
        {
            if (token.context.Count == 2)
            {
                param_value = new List<string>();
                param_value.Add(pipeline_context);
            }
            else
            {
                param_value = token.context.GetRange(1, token.context.Count - 2);
                for (int i = 0; i < param_value.Count; i++)
                {
                    param_value[i] = changeVariableAndWildcardAsValue(param_value[i]);
                }
            }
            FunctionCallUnit run_function = new FunctionCallUnit(function_dictionary, global_variable, token.context[0], param_value);
            run_function.runFunction();
            if (run_function.functionReturn() != null)
            {
                token_return = run_function.functionReturn();
            }
            return token_return;
        }
        /// <summary>
        /// If the value is get value or wildcard, change it to value.
        /// </summary>
        /// <param name="value_string"></param>
        /// <returns></returns>
        private string changeVariableAndWildcardAsValue(string value_string)
        {
            Type type = AnalysesType.analysesValueGetType(value_string);
            if (type == typeof(PWSGetValue))
            {
                if (value_string[0] != '_')
                {
                    value_string = local_variable_dictionary.tryGetDataFromList(value_string);
                }
                else
                {
                    value_string = global_variable.variable_dictionary.tryGetDataFromList(value_string);
                }
            }
            if (type == typeof(PWSWildcard))
            {
                value_string = pipeline_context;
            }
            return value_string;
        }
        public string functionReturn()
        {
            return this.pipeline_context;
        }
    }
}
