using Godot;
using LiteDB;
using System;
using System.Collections.Generic;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class FunctionUnit
    {
        public PWSBaseVariableDictionary local_variable_dictionary = new PWSBaseVariableDictionary();
        public PWSPipelineData pipeline_context = new PWSPipelineData();
        public PWSPipelineData return_value;
        public FunctionUnit(PWSFunctionDictionary function_dictionary, PWSGlobalVariable global_variable, PWSFunctionInformation information, List<string> param_value)
        {
            // add param value
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

            // Virtual CPU
            int pc = 0;
            string token_return = "";
            while (information.body[pc].type != PWSTokenType.EndFunc)
            {
                GD.Print("PC:", pc, "Pipeline:", pipeline_context.popInPipeline());
                PWSInterpreter.PrintList("fu", information.body[pc].context);
                
                switch (information.body[pc].type)
                {
                    case PWSTokenType.Assign:
                        tryAddVariable(global_variable, information.body[pc].context[2], information.body[pc].context[0]);
                        token_return = information.body[pc].context[0];
                        break;
                    case PWSTokenType.ForceAssign:
                        Type from_type = PWSAnalysesType.analysesValueGetType(information.body[pc].context[0]);

                        break;
                    case PWSTokenType.Calculate:
                        if (information.body[pc].context[0] == "<>")
                        {
                            Type type = PWSAnalysesType.analysesValueGetType(information.body[pc].context[2]);
                            information.body[pc].context[0] = pipeline_context.popInPipeline();
                        }
                        if (information.body[pc].context[2] == "<>")
                        {
                            Type type = PWSAnalysesType.analysesValueGetType(information.body[pc].context[0]);
                            information.body[pc].context[2] = pipeline_context.popInPipeline();
                        }
                        if (PWSAnalysesType.analysesValueGetType(information.body[pc].context[0]) == typeof(PWSGetValue))
                        {
                            Type type = PWSAnalysesType.analysesValueGetType(information.body[pc].context[0]);
                            information.body[pc].context[0] = local_variable_dictionary.tryGetDataFromList(information.body[pc].context[0]);
                        }
                        if (PWSAnalysesType.analysesValueGetType(information.body[pc].context[2]) == typeof(PWSGetValue))
                        {
                            Type type = PWSAnalysesType.analysesValueGetType(information.body[pc].context[2]);
                            information.body[pc].context[2] = local_variable_dictionary.tryGetDataFromList(information.body[pc].context[2]);
                        }
                        token_return = CalculateUnit.calculate(information.body[pc].context[0], information.body[pc].context[1], information.body[pc].context[2]);
                        GD.Print("Calculate:", information.body[pc].context[0], information.body[pc].context[1], information.body[pc].context[2], "=", token_return);
                        break;
                    case PWSTokenType.CallFunc:
                        if (information.body[pc].context.Count == 2)
                        {
                            param_value = new List<string>();
                            param_value.Add(pipeline_context.popInPipeline());
                        }
                        FunctionCallUnit run_function = new FunctionCallUnit(function_dictionary, global_variable, information.body[pc].context[0], param_value);
                        run_function.runFunction();
                        if (run_function.functionReturn() != null && run_function.functionReturn().popInPipeline() != null)
                        {
                            token_return = run_function.functionReturn().popInPipeline();
                        }
                        break;
                    case PWSTokenType.EndFunc:
                        return_value = PWSPipelineData.clone(pipeline_context);
                        break;      
                }
                if (information.body[pc].context[information.body[pc].context.Count-1] == "|" || information.body[pc].context[information.body[pc].context.Count-1] == "?")
                    pipeline_context.pushInPipeline(token_return);
                pc++;
            }
        }
        public void tryAddVariable(PWSGlobalVariable global_variable ,string name, string value_string)
        {
            (Type type, object value) = PWSAnalysesType.tryGetTypeObjectByString(value_string);
            GD.Print($"Get Value:{value}");
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
                string add_value = pipeline_context.popInPipeline();
                local_variable_dictionary.addVariableFromString(name, add_value);
                return;
            } 
            local_variable_dictionary.addVariableFromString(name, value_string);
        }
        public PWSPipelineData functionReturn()
        {
            return this.return_value;
        }
    }
}
