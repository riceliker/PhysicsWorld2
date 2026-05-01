using System;
using System.Collections.Generic;
using Godot;
using PhysicsWorld.Src.Terminal.Shell.Compile;

namespace PhysicsWorld.Src.Terminal.Shell
{
    public partial class Interpreter : Node
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

        public enum Region
        {
            obj, sta, str, sh
        }
        public Interpreter()
        {
            string code = @"%% data 5i ;  ## $main ; 0i = a | - 1i | echo ; 0i = i | io.scan | -> i | < 10i ? echo i ; i % 2i | == 0i ? >? ;; echo i ?; #;";
            Lexer lexer = new Lexer(code);
            List<Lexer.Token> tokens = lexer.getTokens();
            MemberSlice memberSlice = new MemberSlice(tokens);
            foreach (string i in memberSlice.GetGlobalVariableList().Keys)
            {
                MemberSlice.VariableData variant_data = memberSlice.GetGlobalVariableList()[i];
                GD.PrintErr(variant_data.member_index," ", variant_data.name," ", variant_data.init_data);
            }
            foreach (string i in memberSlice.GetFunctionDataList().Keys)
            {
                GD.PrintErr(i);
                MemberSlice.FunctionData functionData = memberSlice.GetFunctionDataList()[i];
                foreach (string j in functionData.token_list.Keys)
                {
                    Lexer.Token jt = functionData.token_list[j];
                    GD.PrintErr(jt.ToString());

                }
            }

        }
        public static void PrintList(List<string> i)
        {
            string a = "";
            foreach (string j in i)
            {
                a += j + " ";
            }
            GD.PrintErr(a);
        }
    }
}
