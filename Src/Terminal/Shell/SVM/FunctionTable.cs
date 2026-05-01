using System;
using System.Collections.Generic;
using PhysicsWorld.Src.Terminal.Shell.Compile;

namespace PhysicsWorld.Src.Terminal.SVM
{
    public class FunctionTable
    {
        public class SVMFunction
        {
            public string name;
            public List<string> parameter_list;
            public List<Lexer.Token> tokens;
            public Func<object> execute;
        }
        private readonly Dictionary<string, SVMFunction> _function_list = new();
        
    }
}
