using System.Collections.Generic;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public enum PWSTokenType
    {
        Empty, Assign, Calculate, CallFunc, EndFunc, ForceAssign, Struct
    }
    public class PWSToken
    {
        public List<string> context;
        public PWSTokenType type;
        public PWSToken(List<string> context, PWSTokenType type)
        {
            this.context = context; this.type = type;
        }
        public override string ToString()
        {
            string data = "";
            foreach(string i in context)
            {
                data += i + " ";
            }
            return data;
        }
    }
}
