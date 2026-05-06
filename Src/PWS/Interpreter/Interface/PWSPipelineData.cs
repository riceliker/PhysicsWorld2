using Godot;
using System;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class PWSPipelineData
    {
        private string value;
        public void pushInPipeline(string value_string)
        {
            value = value_string;
        }
        public string popInPipeline()
        {
            return value;
        }
        public static PWSPipelineData clone(PWSPipelineData data)
        {
            var newData = new PWSPipelineData();
            newData.value = data.value;
            return newData;
        }
        public void setValue(string value_string)
        {
            value = value_string;
        }
    }
}

