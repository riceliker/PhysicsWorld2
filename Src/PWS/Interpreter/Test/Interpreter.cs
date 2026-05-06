using System;
using System.Collections.Generic;
using Godot;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    /// <summary>
    /// Developing the new program langrage is a difficult thing.
    /// I don't know what I do.
    /// So I try to run it first, and then write better.
    /// </summary>
    public class PWSInterpreter
    {
        private static Queue<string> output_list = new Queue<string>();
        public static void addOutPut(string mess)
        {
            output_list.Enqueue(mess);
        }
        /// <summary>
        /// used: 
        /// interpreter.getOutPut(msg => {
        ///     string get_text = msg;
        ///     // Other thing...
        /// })
        /// </summary>
        /// <param name="handle_line"></param>
        public static void getOutPut(Action<string> handle_line)
        {
            while (output_list.TryDequeue(out var text))
            {
                handle_line(text);
            }

        }
        public PWSInterpreter()
        {
            string code = @"## $main ; 1 = a ; 2 = b ; 3 = c ;
            a > b ? a = i | <> > c ? $IO.echo <> : $IO.echo c :
            b = i | <> > c ? $IO.echo <> : $IO.echo c ;; 
            #;";
            Lexer lexer = new Lexer(code);
            PWSFunctionDictionary fd = lexer.returnData();
            fd.runFunction("$main", new List<string>());

        }
        public static void PrintList(string info, List<string> i)
        {
            string a = "";
            foreach (string j in i)
            {
                a += j + " ";
            }
            GD.PrintErr(info, ":", a);
        }
    }
}
