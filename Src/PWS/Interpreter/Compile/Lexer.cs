using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    /// <summary>
    ///  When the class run, the code will be sliced.
    ///  Input text script.
    ///  Return global_variable and function_dictionary.
    /// </summary>
    public class Lexer
    {
        List<PWSToken> script_tokens = new List<PWSToken>();
        PWSGlobalVariable global_variable = new PWSGlobalVariable();
        PWSFunctionDictionary function_dictionary = new PWSFunctionDictionary();
        public char[] separators = { ' ', '\t', '\n', '\r' };
        public static string[] token_end_slice = { ";", "|", ";;", "?", "?;", "#;", "`"};
        public static string[] computing = {"+", "-", "*" , "/", "%", ">", "<", ">=", "<=", "==", "?=" , ":=", "!=",
        "!", "&" ,"=" , "->"};
        public static string[] is_struct = {">?", ">!"};
        public static string[] calculate = {"+", "-", "*" , "/", "%", ">", "<", ">=", "<=", "==", "?=" , ":=", "!=",
        "!", "&"};
        public Lexer(string input)
        {
            // 1. Try split token by list `token_end_slice`
            string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<string> text_token_line = new List<string>();
            foreach (string per in words)
            {
                if (token_end_slice.Contains(per))
                {
                    text_token_line.Add(per);
                    PWSToken token = new PWSToken(text_token_line, PWSTokenType.Empty);
                    script_tokens.Add(token);
                    text_token_line = new List<string>();
                }
                else
                {
                    text_token_line.Add(per);
                }
            }

            // 2. Add `<>` to make analyses token easily
            for(int i = 0; i < script_tokens.Count; i++)
            {
                PWSToken token = script_tokens[i];
                if (token.context.Count == 3 && computing.Contains(token.context[0]))
                {
                    token.context.Insert(0,"<>");
                }
                script_tokens[i] = token;
            }
            GD.Print(script_tokens.Count);
            // 3. classify token is global variable, function or annotation
            // The key structure in this code is state machine.
            // `is_function` is judge that this token is in function body.
            // When meet `##` start function. When meet `#;` mean end function,.
            int member_index = 0;
            PWSFunctionInformation currentFunction = null;
            foreach(PWSToken i in script_tokens)
            {
                PWSInterpreter.PrintList("loop",i.context);
                PWSFunctionInformation function_data = new PWSFunctionInformation();
                // mean annotation, ignore token
                if (i.context[0] == "`")
                {
                    continue;
                }
                // mean global value
                if (i.context[0] == "%%")
                {
                    if (i.context.Count == 4)
                    {
                        string variable_name = i.context[1];
                        string value_string = i.context[2];
                        try
                        {
                            global_variable.variable_dictionary.addVariableFromString(variable_name, value_string);
                        }
                        catch
                        {
                            PWSInterpreter.addOutPut($"Building Error In {member_index}: global variable is repetition");
                            GD.PushWarning("ERROR");
                        }
                    }
                    else
                    {
                        PWSInterpreter.addOutPut($"Building Error In {member_index}: Error grammar about new global variable");
                        GD.PushWarning("ERROR");
                    }
                    continue;
                }
                // mean start record function, so push old function and create new。
                if (i.context[0] == "##")
                {
                    if (currentFunction != null && !string.IsNullOrEmpty(currentFunction.name))
                    {
                        if (!function_dictionary.function_dictionary.ContainsKey(currentFunction.name))
                        {
                            function_dictionary.function_dictionary.Add(currentFunction.name, currentFunction);
                        }
                        else
                        {
                            PWSInterpreter.addOutPut($"Building Error In {member_index}: The same function name");
                        }
                    }
                    currentFunction = new PWSFunctionInformation();
                    currentFunction.name = i.context[1];
                    currentFunction.body = new List<PWSToken>();
                   
                    // param list
                    if (i.context.Count > 3)
                    {
                        int j = 2;
                        while (true)
                        {
                            if (i.context[j] == ";") break;
                            currentFunction.parament_name.Add(i.context[j]);
                            j++;
                        }
                        
                    }
                    GD.Print("Func:",currentFunction.parament_name.Count);
                    continue;
                }

                // Now you can add token in function, and classify the token type
                if (currentFunction != null)
                {
                    currentFunction.body ??= new List<PWSToken>();
                    if (!currentFunction.body.Contains(i))
                    {
                        // if not any type, mean CallFunc
                        i.type = PWSTokenType.CallFunc;
                        if (i.context.Count == 1)
                        {
                            if (i.context[0] == "#;")
                                i.type = PWSTokenType.EndFunc;
                            
                        }
                        if (i.context.Count == 2 && is_struct.Contains(i.context[0]))
                            i.type = PWSTokenType.Struct;
                        if (i.context.Count == 4)
                        {
                            if (calculate.Contains(i.context[1]))
                                i.type = PWSTokenType.Calculate;
                            if (i.context[1] == "=")
                                i.type = PWSTokenType.Assign;  
                            if (i.context[1] == "->")
                                i.type = PWSTokenType.ForceAssign;
                        }
                        currentFunction.body.Add(i);
                    }
                        
                }
                member_index++;
            }
            // When the loop end, the last function need store
            if (currentFunction != null && !string.IsNullOrEmpty(currentFunction.name))
            {
                if (!function_dictionary.function_dictionary.ContainsKey(currentFunction.name))
                {
                    function_dictionary.function_dictionary.Add(currentFunction.name, currentFunction);
                }
                else
                {
                    PWSInterpreter.addOutPut("Building Error: The same function name");
                    GD.PushWarning("ERROR");
                }
            }
        }
        public PWSFunctionDictionary returnData()
        {
            function_dictionary.global_variable = global_variable;
            return function_dictionary;
        }
    }

}
