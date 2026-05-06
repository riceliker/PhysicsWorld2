using Godot;
using System;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class PWSPipelineData
    {
        private int maybe_int = 0;
        private float maybe_float = 0.0f;
        private string maybe_string = "";
        private bool maybe_bool = false;
        public void pushInPipeline(Type type, object value)
        {
            switch (type)
            {
                case Type _ when type == typeof(int):
                    (bool success0, object object0) = PWSAnalysesType.TryChangeData(value, type, typeof(int));
                    maybe_int = (int)object0;
                    break;
                case Type _ when type == typeof(float):
                    (bool success1, object object1) = PWSAnalysesType.TryChangeData(value, type, typeof(float));
                    maybe_int = (int)object1;
                    break;
                case Type _ when type == typeof(string):
                    (bool success2, object object2) = PWSAnalysesType.TryChangeData(value, type, typeof(string));
                    maybe_int = (int)object2;
                    break;
                case Type _ when type == typeof(bool):
                    (bool success3, object object3) = PWSAnalysesType.TryChangeData(value, type, typeof(bool));
                    maybe_int = (int)object3;
                    break;

            }
        }
        public string popInPipeline(Type type)
        {
            switch (type)
            {
                case Type _ when type == typeof(int):
                    return PWSAnalysesType.TryGetStringByTypeObject(typeof(int), (object)maybe_int);
                case Type _ when type == typeof(float):
                    return PWSAnalysesType.TryGetStringByTypeObject(typeof(float), (object)maybe_float);
                case Type _ when type == typeof(string):
                    return PWSAnalysesType.TryGetStringByTypeObject(typeof(string), (object)maybe_string);
                case Type _ when type == typeof(bool):
                    return PWSAnalysesType.TryGetStringByTypeObject(typeof(bool), (object)maybe_bool);
                default:
                    return default;
            }
        }
    }
}
