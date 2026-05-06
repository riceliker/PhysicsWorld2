using Godot;
using System;
using System.Collections.Generic;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class PWSBaseVariableDictionary
    {
        public Dictionary<string, int> int_variable_list = new Dictionary<string, int>();
        public Dictionary<string, float> float_variable_list = new Dictionary<string, float>();
        public Dictionary<string, string> string_variable_list = new Dictionary<string, string>();
        public Dictionary<string, bool> bool_variable_list = new Dictionary<string, bool>();
        public (bool, object) tryGetDataFromList(Type type, string name)
        {
            bool success = true;
            object result = default;
            switch (type)
            {
                case Type _ when type == typeof(int):
                    if(int_variable_list.TryGetValue(name, out var value0)) result = (object)value0;
                    else success = false;
                    break;
                case Type _ when type == typeof(float):
                    if(float_variable_list.TryGetValue(name, out var value1)) result = (object)value1;
                    else success = false;
                    break;
                case Type _ when type == typeof(string):
                    if(string_variable_list.TryGetValue(name, out var value2)) result = (object)value2;
                    else success = false;
                    break;
                case Type _ when type == typeof(bool):
                    if(bool_variable_list.TryGetValue(name, out var value3)) result = (object)value3;
                    else success = false;
                    break;
                default:
                    success = false;
                    result = default;
                    break;
            };
            return (success, result);
        }
        public bool trySetDataFromList(Type type, string name, object value)
        {
            bool success = true;
            switch (type)
            {
                case Type _ when type == typeof(int):
                    if(int_variable_list.TryGetValue(name, out var value0)) int_variable_list[name] = value0;
                    else success = false;
                    break;
                case Type _ when type == typeof(float):
                    if(float_variable_list.TryGetValue(name, out var value1)) float_variable_list[name] = value1;
                    else success = false;
                    break;
                case Type _ when type == typeof(string):
                    if(string_variable_list.TryGetValue(name, out var value2)) string_variable_list[name] = value2;
                    else success = false;
                    break;
                case Type _ when type == typeof(bool):
                    if(bool_variable_list.TryGetValue(name, out var value3)) bool_variable_list[name] = value3;
                    else success = false;
                    break;
                default:
                    success = false;
                    break;
            };
            return success;
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
            (Type type, object value) = PWSAnalysesType.analysesValueGetType(value_string);
            switch(type)
            {
                case Type _ when type == typeof(int):
                    int_variable_list.Add(name, (int)value);
                    break; 
                case Type _ when type == typeof(float):
                    float_variable_list.Add(name, (float)value);
                    break;
                case Type _ when type == typeof(string):
                    string_variable_list.Add(name, (string)value);
                    break;
                case Type _ when type == typeof(bool):
                    bool_variable_list.Add(name, (bool)value);
                    break;
            }
        }

    }
}
