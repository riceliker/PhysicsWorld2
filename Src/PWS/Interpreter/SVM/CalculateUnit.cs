using Godot;
using System;
using System.Linq;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public static class CalculateUnit
    {
        public static string calculate(string a, string ope, string b)
        {
            if (PWSAnalysesType.analysesValueGetType(a) != PWSAnalysesType.analysesValueGetType(b))
            {
                if (ope == ":=")
                {
                    return PWSAnalysesType.tryGetStringByTypeObject<bool>(false);
                }
                throw new($"Building Error In {a} {ope} {b}: The calculate type was not same");
            }
            if (ope == ":=")
            {
                return PWSAnalysesType.tryGetStringByTypeObject<bool>(true);
            }
            Type type = PWSAnalysesType.analysesValueGetType(a);
            // Basic Calculate
            string[] basic_ope = ["+", "-", "*", "/", "%"];
            if (basic_ope.Contains(ope))
            {
                
                switch (type)
                {
                    case Type _ when type == typeof(int):
                        int int_a = PWSAnalysesType.tryGetTypeObjectByString<int>(a);
                        int int_b = PWSAnalysesType.tryGetTypeObjectByString<int>(b);
                        int int_o = basicCalculate<int>(int_a, ope, int_b);
                        return PWSAnalysesType.tryGetStringByTypeObject<int>(int_o);
                    case Type _ when type == typeof(float):
                        float float_a = PWSAnalysesType.tryGetTypeObjectByString<float>(a);
                        float float_b = PWSAnalysesType.tryGetTypeObjectByString<float>(b);
                        float float_o = basicCalculate<float>(float_a, ope, float_b);
                        return PWSAnalysesType.tryGetStringByTypeObject<float>(float_o);
                    case Type _ when type == typeof(string):
                        string string_a = PWSAnalysesType.tryGetTypeObjectByString<string>(a);
                        string string_b = PWSAnalysesType.tryGetTypeObjectByString<string>(b);
                        string string_o = basicCalculate<string>(string_a, ope, string_b);
                        return PWSAnalysesType.tryGetStringByTypeObject<string>(string_o);
                    case Type _ when type == typeof(bool):
                        bool bool_a = PWSAnalysesType.tryGetTypeObjectByString<bool>(a);
                        bool bool_b = PWSAnalysesType.tryGetTypeObjectByString<bool>(b);
                        bool bool_o = basicCalculate<bool>(bool_a, ope, bool_b);
                        return PWSAnalysesType.tryGetStringByTypeObject<bool>(bool_o);
                    default:
                        return "";
                }
            }
            // compare calculate
            string[] compare_ope = ["&", "|", "^", "!", "==", "!=", ">", "<", ">=", "<="];
            if (compare_ope.Contains(ope))
            {
                Type type1 = PWSAnalysesType.analysesValueGetType(a);
                switch (type1)
                {
                    case Type _ when type1 == typeof(int):
                        int int_a = PWSAnalysesType.tryGetTypeObjectByString<int>(a);
                        int int_b = PWSAnalysesType.tryGetTypeObjectByString<int>(b);
                        bool int_o = compareCalculate<int>(a, ope, b);
                        return PWSAnalysesType.tryGetStringByTypeObject<bool>(int_o);
                    case Type _ when type1 == typeof(float):
                        float float_a = PWSAnalysesType.tryGetTypeObjectByString<float>(a);
                        float float_b = PWSAnalysesType.tryGetTypeObjectByString<float>(b);
                        bool float_o = compareCalculate<float>(a, ope, b);
                        return PWSAnalysesType.tryGetStringByTypeObject<bool>(float_o);
                    case Type _ when type1 == typeof(string):
                        string string_a = PWSAnalysesType.tryGetTypeObjectByString<string>(a);
                        string string_b = PWSAnalysesType.tryGetTypeObjectByString<string>(b);
                        bool string_o = compareCalculate<string>(a, ope, b);
                        return PWSAnalysesType.tryGetStringByTypeObject<bool>(string_o);
                    case Type _ when type1 == typeof(bool):
                        bool bool_a = PWSAnalysesType.tryGetTypeObjectByString<bool>(a);
                        bool bool_b = PWSAnalysesType.tryGetTypeObjectByString<bool>(b);
                        bool bool_o = compareCalculate<bool>(a, ope, b);
                        return PWSAnalysesType.tryGetStringByTypeObject<bool>(bool_o);
                    default:
                        return "";
                }
                throw new($"Building Error In {a} {ope} {b}: No support calculate operation");
            }
            // bool calculate
            string[] bool_ope = ["&", "+", "^", "!"];
            if (bool_ope.Contains(ope))
            {
                if (type != typeof(bool))
                {
                    throw new($"Building Error In {a} {ope} {b}: No support calculate operation");
                }
                bool bool_a = PWSAnalysesType.tryGetTypeObjectByString<bool>(a);
                bool bool_b = PWSAnalysesType.tryGetTypeObjectByString<bool>(b);
                bool bool_o = false;
                switch (ope)
                {
                    case "&":
                        bool_o = (bool)(object)a && (bool)(object)b;
                        break;
                    case "+":
                        bool_o = (bool)(object)a || (bool)(object)b;
                        break;
                    case "^":
                        bool_o = (bool)(object)a ^ (bool)(object)b;
                        break;
                    case "!":
                        bool_o = ! (bool)(object)b;
                        break;
                    default:
                        bool_o = false;
                        break;
                }
                return PWSAnalysesType.tryGetStringByTypeObject<bool>(bool_o);
            }
            throw new($"Building Error In {a} {ope} {b}: No support calculate operation");
        }
        private static T basicCalculate<T>(T a, string ope, T b)
        {
            Type[] number_calculate = {typeof(int), typeof(float), typeof(string)};
            if (number_calculate.Contains(typeof(T)))
            {
                Type[] allow_plus = {typeof(int), typeof(float), typeof(string)};
                if (allow_plus.Contains(typeof(T)) && ope == "+")
                {
                    return (T) plus(typeof(T), a, b); 
                }
                Type[] allow_minus = {typeof(int), typeof(float)};
                if (allow_minus.Contains(typeof(T)) && ope == "-")
                {
                    return (T) minus(typeof(T), a, b); 
                }
                Type[] allow_multi = {typeof(int), typeof(float)};
                if (allow_multi.Contains(typeof(T)) && ope == "*")
                {
                    return (T) multi(typeof(T), a, b);
                }
                Type[] allow_div = {typeof(int), typeof(float)};
                if (allow_div.Contains(typeof(T)) && ope == "/")
                {
                    return (T) div(typeof(T), a, b);
                }
                if (typeof(T) == typeof(int) && ope == "%")
                {
                    return (T)(object)((int)(object)a % (int)(object)b);  
                }
            }
            
            throw new($"Building Error In {a} {ope} {b}: No support calculate operation");
        }
        public static bool compareCalculate<T>(string sa, string ope, string sb)
        {
            T a = PWSAnalysesType.tryGetTypeObjectByString<T>(sa);
            T b = PWSAnalysesType.tryGetTypeObjectByString<T>(sb);
            if (ope == "==")
            {
                return a.Equals(b);
            }
            if (ope == "!=")
            {
                return !a.Equals(b);
            }
            if (ope == ">")
            {
                if (typeof(T) == typeof(int))
                {
                    return (int)(object)a > (int)(object)b;
                }
                if (typeof(T) == typeof(float))
                {
                    return (float)(object)a > (float)(object)b;
                }
            }
            if (ope == "<")
            {
                if (typeof(T) == typeof(int))
                {
                    return (int)(object)a < (int)(object)b;
                }
                if (typeof(T) == typeof(float))
                {
                    return (float)(object)a < (float)(object)b;
                }
            }
            if (ope == ">=")
            {
                if (typeof(T) == typeof(int))
                {
                    return (int)(object)a >= (int)(object)b;
                }
                if (typeof(T) == typeof(float))
                {
                    return (float)(object)a >= (float)(object)b;
                }
            }
            if (ope == "<=")
            {
                if (typeof(T) == typeof(int))
                {
                    return (int)(object)a <= (int)(object)b;
                }
                if (typeof(T) == typeof(float))
                {
                    return (float)(object)a <= (float)(object)b;
                }
            }
            throw new($"Building Error In {a} {ope} {b}: No support calculate operation");
        }
        public static object plus(Type type, object a, object b)
        {
            try
            {
                object res = type.Name switch
                {
                    nameof(Int32) => (int)a + (int)b,
                    nameof(Single) => (float)a + (float)b,
                    nameof(String) => (string)a + (string)b,
                    _ => null
                };

                return res;
            }
            catch
            {
                return null;
            }
        }
        public static object minus(Type type, object a, object b)
        {
            try
            {
                object res = type.Name switch
                {
                    nameof(Int32) => (int)a - (int)b,
                    nameof(Single) => (float)a - (float)b,
                    _ => null
                };

                return res;
            }
            catch
            {
                return null;
            }
        }
        public static object multi(Type type, object a, object b)
        {
            try
            {
                object res = type.Name switch
                {
                    nameof(Int32) => (int)a * (int)b,
                    nameof(Single) => (float)a * (float)b,
                    _ => null
                };

                return res;
            }
            catch
            {
                return null;
            }
        }
        public static object div(Type type, object a, object b)
        {
            try
            {
                object res = type.Name switch
                {
                    nameof(Int32) => (int)a / (int)b,
                    nameof(Single) => (float)a / (float)b,
                    _ => null
                };

                return res;
            }
            catch
            {
                return null;
            }
        }
    }
}
