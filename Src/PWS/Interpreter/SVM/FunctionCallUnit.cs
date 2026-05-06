using System.Collections.Generic;
using Godot;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class FunctionCallUnit
    {
        public PWSPipelineData pipeline_data;
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
            pipeline_data = new PWSPipelineData();
            if (function_dictionary.function_dictionary.ContainsKey(function_name))
            {   
                FunctionUnit function_unit = new FunctionUnit(function_dictionary, global_variable, function_dictionary.function_dictionary[function_name], param_value);
                pipeline_data = function_unit.functionReturn();
            }
            string[] not_user_functions = function_name.Split(".");
            if (not_user_functions[0] == "$io")
            {
                switch (not_user_functions[1])
                {
                    case "scan":
                        pipeline_data.pushInPipeline(PWSIO.scan());
                        break;
                    case "echo":
                        PWSIO.echo(param_value[0]);
                        break;
                }
            }
            
        }
        public PWSPipelineData functionReturn()
        {
            return pipeline_data;
        }
    }
            
}
