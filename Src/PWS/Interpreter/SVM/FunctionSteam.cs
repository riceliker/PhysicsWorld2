using Godot;
using System.Collections.Generic;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class FunctionSteam
    {
        public Dictionary<string, VariableData> global_variable = new Dictionary<string, VariableData>();
        public Dictionary<string, FunctionData> function_list = new Dictionary<string, FunctionData>();
        public FunctionSteam(Dictionary<string, VariableData> global_variable, Dictionary<string, FunctionData> function_list)
        {
            this.global_variable = global_variable;
            this.function_list = function_list;
            findMainFunction();
        }
        public void findMainFunction()
        {
            foreach(string func_name in function_list.Keys)
            {
                if (func_name == "$main")
                {
                    FunctionData function_data = function_list.GetValueOrDefault(func_name);
                    runFunction("$main", new List<object>());
                }
            }
        }
        public void runFunction(string name, List<object> param_value_list)
        {
            FunctionData data = function_list.GetValueOrDefault(name);
            Dictionary<string, object> local_variable = new Dictionary<string, object>();
            List<string> param_name_list = new List<string>();
            Dictionary<string, object> param_list = new Dictionary<string, object>();
            int count = 0;
            foreach(string param_name in param_name_list)
            {
                param_list.Add(param_name, param_value_list[count]);
            }

            int pc = 0;
            while (data.token_list[pc].type != Lexer.TokenType.EndFunc)
            {
                
                PrintList(data.token_list[pc].context);
                switch (data.token_list[pc].type)
                {
                    case Lexer.TokenType.Assign:
                        makeNewVariable(data.token_list[pc].context, local_variable);
                        break;
                }
                pc++;
            }



        }
        public void makeNewVariable(List<string> token, Dictionary<string, object> local)
        {
            string variable_name = token[2];
            string variable_string = token[0];
            object variable = "";
            switch (variable_string[variable_string.Length - 1])
            {
                case 'i':
                    variable = variable_string.Substring(0,variable_string.Length - 1).ToInt();
                    GD.Print(variable);
                    break;
            }
            local.Add(variable_name, variable);
        }
        public static void PrintList(List<string> i)
        {
            string a = "";
            foreach (string j in i)
            {
                a += j + " ";
            }
            GD.PrintErr(a);
        }
    }
}
