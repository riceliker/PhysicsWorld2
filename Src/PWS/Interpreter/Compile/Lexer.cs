using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    /// <summary>
    ///  When the class run, the code will be sliced.
    ///  Return List<Token> tokens
    /// </summary>
    public class Lexer
    {
        public enum TokenType
        {
            Empty, Assign, Calculate, CallFunc, EndFunc, ForceAssign, Struct
        }
        public class Token
        {
            public int row_number;
            public readonly List<string> context;
            public TokenType type;
            public Token(int row_number, TokenType type, List<string> context)
            {
                this.row_number = row_number; this.type = type; this.context = context;
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
        List<Token> tokens = new List<Token>();
        public char[] separators = { ' ', '\t', '\n', '\r' };
        public static string[] token_end_slice = { ";", "|", ";;", "?", "?;", "#;"};
        public static string[] computing = {"+", "-", "*" , "/", "%", ">", "<", ">=", "<=", "==", "?=" , ":=", "!=",
        "!", "&" ,"=" , "->"};
        public Lexer(string input)
        {
            makeToken(input);
            addPipeWildcard();
        }
        private void makeToken(string input)
        {
            string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<Token> tokens = new List<Token>();
            List<string> a_token_line = new List<string>();
            foreach (string per in words)
            {
                if (token_end_slice.Contains(per))
                {
                    a_token_line.Add(per);
                    Token token = new Token(-1, TokenType.Empty, a_token_line);
                    tokens.Add(token);
                    a_token_line = new List<string>();
                }
                else
                {
                    a_token_line.Add(per);
                }
            }
            this.tokens = tokens;
        }
        // I allow the user that ignore the `<>` if in the head of statement.
        // But in Interpreter, 
        private void addPipeWildcard()
        {
            
            for(int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if (token.context.Count == 3 && computing.Contains(token.context[0]))
                {
                    token.context.Insert(0,"<>");
                }
                //tokens[i] = token;
            }
        } 
        public List<Token> getTokens()
        {
            return tokens;
        }
    }

}
