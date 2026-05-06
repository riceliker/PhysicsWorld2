using Godot;
using System;
using System.Collections.Generic;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class PWSFunctionInformation
    {
        public string name;
        public List<string> parament_name = new List<string>();
        public List<PWSToken> body;

    }
    public class PWSFunctionDictionary
    {
        public PWSGlobalVariable global_variable {get;set;}
        public Dictionary<string, PWSFunctionInformation> function_dictionary = new Dictionary<string, PWSFunctionInformation>();
        public void runFunction(string function_name, List<string> param_list)
        {
            if (function_dictionary.TryGetValue(function_name, out var result))
            {
                FunctionUnit function_unit = new FunctionUnit(this , global_variable, result, param_list);
            }
            
        }
    }
}
