using Godot;
using System;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class PWSWildcard;
    public class PWSGetValue;
    public static class PWSAnalysesType
    {
        /// <summary>
        /// value_string -> type, object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static (Type, object) tryGetTypeObjectByString(string value)
        {
            if (value.StartsWith('"') && value.EndsWith('"'))
            {
                string str = value.Trim('"');
                return (typeof(string), str); 
            }

            if (value.EndsWith('i') && value.Length > 1 && (char.IsDigit(value[0]) || value[0] == '-'))
            {
                string numStr = value[0..^1];
                int num = int.Parse(numStr);
                return (typeof(int), num);
            }

            // float
            if (value.EndsWith('f') && value.Length > 1 && (char.IsDigit(value[0]) || value[0] == '-'))
            {
                string numStr = value[0..^1];
                float num = float.Parse(numStr);
                return (typeof(float), num);
            }
            if (value == "true" || value == "false")
            {
                bool boolean = bool.Parse(value);
                return (typeof(bool), boolean);
            }
            if (value == "<>")
            {
                return (typeof(PWSWildcard), null);
            }
            return (typeof(PWSGetValue), null);    
        }
        public static T tryGetTypeObjectByString<T>(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default;
            if (value.StartsWith('"') && value.EndsWith('"'))
            {
                string str = value.Trim('"');
                return (T)(object)str; 
            }

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
            if (value == "true" || value == "false")
            {
                bool boolean = bool.Parse(value);
                return (T)(object)boolean;
            }
            if (value == "<>")
            {
                return (T)(object)new PWSWildcard();
            }
            return (T)(object)new PWSGetValue();    
        }
        public static string tryGetStringByTypeObject<T>(object value)
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
        public static string tryGetStringByTypeObject(Type type, object value)
        {
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
        /// <summary>
        /// value_string -> type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Type analysesValueGetType(string value)
        {
            if (value.StartsWith('"') && value.EndsWith('"'))
            {
                return typeof(string); 
            }

            if (value.EndsWith('i') && value.Length > 1 && (char.IsDigit(value[0]) || value[0] == '-'))
            {
                return typeof(int);
            }

            // float
            if (value.EndsWith('f') && value.Length > 1 && (char.IsDigit(value[0]) || value[0] == '-'))
            {
                return typeof(float);
            }
            if (value == "true" || value == "false")
            {
                return typeof(bool);
            }
            if (value == "<>")
            {
                return typeof(PWSWildcard);
            }
            return typeof(PWSGetValue);
            
            
        }
        /// <summary>
        /// change type
        /// </summary>
        /// <param name="value_string"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string TryChangeData(string value_string, Type to)
        {
            (Type type, object result) = tryGetTypeObjectByString(value_string);
            return tryGetStringByTypeObject(to, result);
        }
        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
        
    }
}
