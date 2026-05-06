using Godot;
using System;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class PipelineData
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
        public static PipelineData clone(PipelineData data)
        {
            var newData = new PipelineData();
            newData.value = data.value;
            return newData;
        }
    }
}

