using System;
using System.Collections.Generic;
using Godot;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    /// <summary>
    /// When the function call new function, it will create a new function unit to run the function.
    /// You must new class first, and then run the function, and then get the return value.
    /// </summary>
    public class FunctionCallUnit
    {
        public string pipeline_data;
        PWSFunctionDictionary function_dictionary;
        PWSGlobalVariable global_variable;
        string function_name;
        List<string> param_value;
        public FunctionCallUnit(PWSFunctionDictionary function_dictionary, PWSGlobalVariable global_variable, string function_name, List<string> param_value)
        {
            this.function_dictionary = function_dictionary;
            this.global_variable = global_variable;
            this.function_name = function_name;
            this.param_value = param_value;
        }
        public void runFunction()
        {
            pipeline_data = "";
            if (function_dictionary.function_dictionary.ContainsKey(function_name))
            {   
                FunctionUnit function_unit = new FunctionUnit(function_dictionary, global_variable, function_dictionary.function_dictionary[function_name], param_value);
                pipeline_data = function_unit.functionReturn();
            }
            string[] not_user_functions = function_name.Split(".");
            if (not_user_functions[0] == "$IO")
            {
                switch (not_user_functions[1])
                {
                    case "scan":
                        pipeline_data = PWSIO.scan();
                        break;
                    case "echo":
                        string output_string = param_value[0];
                        Type type = AnalysesType.analysesValueGetType(output_string);
                        switch (type)
                        {
                            case Type t when t == typeof(int):
                                output_string = output_string.Substring(0, output_string.Length - 1);
                                break;
                            case Type t when t == typeof(float):
                                output_string = output_string.Substring(0, output_string.Length - 1);
                                break;
                            case Type t when t == typeof(string):
                                output_string = output_string.Trim('\'');
                                break;
                            case Type t when t == typeof(bool):
                                break;
                        }
                        PWSIO.echo(output_string);
                        break;
                }
            }
            
        }
        public string functionReturn()
        {
            return pipeline_data;
        }
    }
            
}
