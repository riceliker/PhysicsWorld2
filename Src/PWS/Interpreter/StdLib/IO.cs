using Godot;
using System;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public static class PWSIO
    {
        public static string scan()
        {
            return "\"6\"";
        }
        public static void echo(string mess)
        {
            PWSInterpreter.addOutPut(mess);
            GD.Print("IO:",mess);
        }
    }
}