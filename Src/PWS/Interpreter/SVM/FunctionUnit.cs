using Godot;
using System;
using System.Collections.Generic;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class FunctionUnit
    {
        public PWSBaseVariableDictionary local_variable_dictionary = new PWSBaseVariableDictionary();
        public PWSPipelineData pipeline_context = new PWSPipelineData();
        public FunctionUnit(PWSGlobalVariable global_variable, PWSFunctionInformation information, List<string> param_value)
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
            int pc = 0;

            while (information.body[pc].type != PWSTokenType.EndFunc)
            {
                
                PWSInterpreter.PrintList("fu", information.body[pc].context);
                switch (information.body[pc].type)
                {
                    case PWSTokenType.Assign:
                        tryAddVariable(information.body[pc].context[2], information.body[pc].context[0]);
                        break;
                    case PWSTokenType.ForceAssign:
                        break;

                }
                pc++;
            }
        }
        public void tryAddVariable(string name, string value_string)
        {
            (Type type, object value) = PWSAnalysesType.analysesValueGetType(value_string);
            if (type == typeof(PWSFunction))
            {
                return;
            }
            if (type == typeof(PWSWildcard))
            {
                object add_value = pipeline_context.popInPipeline(type);
                local_variable_dictionary.addVariableFromString(name, value_string);
                return;
            }
            local_variable_dictionary.addVariableFromString(name, value_string);
        }
    }
}
