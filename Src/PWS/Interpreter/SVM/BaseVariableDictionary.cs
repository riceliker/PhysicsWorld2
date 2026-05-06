using Godot;
using System;
using System.Collections.Generic;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    /// <summary>
    /// This class will store the variable in the function unit, and try to analyses the type of variable by the string.
    /// </summary>
    public class BaseVariableDictionary
    {
        public Dictionary<string, string> value_string_variable_list = new Dictionary<string, string>();
        public string tryGetDataFromList(string name)
        {
            if (value_string_variable_list.TryGetValue(name, out var value))
            {
                return value;
            }
            return null;
        }
        public bool trySetDataFromList(string name, string value_string)
        {
            if (value_string_variable_list.ContainsKey(name))
            {
                value_string_variable_list[name] = value_string;
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// If get a new base variable, try to infer the type
        /// 5i : int; 3.14f : float; true/false : bool; other string;
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value_string"></param>
        /// <returns></returns>
        public void addVariableFromString(string name, string value_string)
        {
            value_string_variable_list.Add(name, value_string);
        }

    }
}
