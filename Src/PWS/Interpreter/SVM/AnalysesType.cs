using Godot;
using System;

namespace PhysicsWorld.Src.PWS.Interpreter
{ 
    /// <summary>
    /// mean `<>`, depend on the pipeline context.
    /// </summary>
    public class PWSWildcard;
    /// <summary>
    /// mean variable, depend on the variable dictionary.
    /// </summary>
    public class PWSGetValue;
    /// <summary>
    /// To make store the variable more easily, I want to analyses the type of variable by the string.
    /// For example, `5i` is int, `3.14f` is float, `"hello"` is string, `true`/`false` is bool.
    /// when swap data, you will never use generic or type + object. Just string.
    /// </summary>
    public static class AnalysesType
    {
        /// <summary>
        /// value_string -> type
        /// this method will analyses the type of value by the string, and return the type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Type analysesValueGetType(string value)
        {
            // string
            if (value.StartsWith('\'') && value.EndsWith('\''))
            {
                return typeof(string); 
            }
            // int
            if (value.EndsWith('i') && value.Length > 1 && (char.IsDigit(value[0]) || value[0] == '-'))
            {
                return typeof(int);
            }
            // float
            if (value.EndsWith('f') && value.Length > 1 && (char.IsDigit(value[0]) || value[0] == '-'))
            {
                return typeof(float);
            }
            // bool
            if (value == "true" || value == "false")
            {
                return typeof(bool);
            }
            // wildcard
            if (value == "<>")
            {
                return typeof(PWSWildcard);
            }
            // if not match, return PWSGetValue, which means it is a variable.
            return typeof(PWSGetValue); 
        }
        /// <summary>
        /// This method will analyses the type of value by the string, and return the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T tryGetTypeObjectByString<T>(string value)
        {
            // if the value is null or whitespace, return default value of T
            if (string.IsNullOrWhiteSpace(value))
                return default;
            // string
            if (value.StartsWith('\'') && value.EndsWith('\''))
            {
                string str = value.Trim('\'');
                return (T)(object)str; 
            }
            // int
            if (value.EndsWith('i') && value.Length > 1 && (char.IsDigit(value[0]) || value[0] == '-'))
            {
                string numStr = value[0..^1];
                if (string.IsNullOrWhiteSpace(numStr))
                    return default;

                if (int.TryParse(numStr, out int num))
                    return (T)(object)num;
            }
            // float
            if (value.EndsWith('f') && value.Length > 1 && (char.IsDigit(value[0]) || value[0] == '-'))
            {
                string numStr = value[0..^1];
                if (string.IsNullOrWhiteSpace(numStr))
                    return default;
                if (float.TryParse(numStr, out float num))
                    return (T)(object)num;
            }
            // bool
            if (value == "true" || value == "false")
            {
                bool boolean = bool.Parse(value);
                return (T)(object)boolean;
            }
            // wildcard
            if (value == "<>")
            {
                return (T)(object)new PWSWildcard();
            }
            // if not match, return PWSGetValue, which means it is a variable.
            return (T)(object)new PWSGetValue();    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string tryGetStringByTypeObject<T>(T value)
        {
            Type type = typeof(T);
            switch(type)
            {
                case Type _ when type == typeof(int):
                    return value.ToString()+"i";
                case Type _ when type == typeof(float):
                    return value.ToString()+"f";
                case Type _ when type == typeof(string):
                    return value.ToString();
                case Type _ when type == typeof(bool):
                    return value.ToString().ToLower();
                default:
                    return "";
            }
        }
        public static string forceChangeType(string from, string to)
        {
            object temp_value;
            Type from_type = analysesValueGetType(from);
            switch(from_type)
            {
                case Type _ when from_type == typeof(int):
                    temp_value = from.Substring(from.Length-1);
                    break;
                case Type _ when from_type == typeof(float):
                    temp_value = from.Substring(from.Length-1);
                    break;
                case Type _ when from_type == typeof(string):
                    temp_value = from.Substring(1,from.Length-2);
                    break;
                case Type _ when from_type == typeof(bool):
                    temp_value = bool.Parse(from);
                    break;
                default:
                    temp_value = null;
                    break;
            }
            Type to_type = analysesValueGetType(to);
            switch(to_type)
            {
                case Type _ when to_type == typeof(int):
                    return temp_value.ToString()+"i";
                case Type _ when to_type == typeof(float):
                    return temp_value.ToString()+"f";
                case Type _ when to_type == typeof(string):
                    return '\'' + temp_value.ToString() + '\'';
                case Type _ when to_type == typeof(bool):
                    return temp_value.ToString().ToLower();
                default:
                    return "";
            }
        }
        
    }
}
