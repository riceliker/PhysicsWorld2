using Godot;
using System;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class PWSWildcard;
    public class PWSFunction;
    public static class PWSAnalysesType
    {
        public static (Type, object) analysesValueGetType(string value)
        {
            if (value.StartsWith('"') && value.EndsWith('"'))
            {
                string str = value.Trim('"');
                return (typeof(string), str); 
            }

            if (value.EndsWith('i'))
            {
                string numStr = value[0..^1];
                int num = int.Parse(numStr);
                return (typeof(int), num);
            }

            // float
            if (value.EndsWith('f'))
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
            return (typeof(PWSFunction), null);
            
            
        }
        /// <summary>
        /// If the value will change T to U
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <returns></returns>
        public static (bool success, object result) TryChangeData(object value, Type from, Type to)
        {
            try
            {
                if (value == null)
                {
                    return (false, to.IsValueType ? Activator.CreateInstance(to) : null);
                }

                if (from == to)
                {
                    return (true, value);
                }

                if (to == typeof(string))
                {
                    return (true, value.ToString() ?? "");
                }

                if (value is IConvertible convertible)
                {
                    object result = Convert.ChangeType(value, to);
                    return (true, result);
                }

                return (false, GetDefaultValue(to));
            }
            catch
            {
                return (false, GetDefaultValue(to));
            }
        }

        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
        public static string TryGetStringByTypeObject(Type type, object value)
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
                    return value.ToString();
                default:
                    return "";
            }
        }
        
    }
}
