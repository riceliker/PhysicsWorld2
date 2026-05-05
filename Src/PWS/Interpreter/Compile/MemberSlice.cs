using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace PhysicsWorld.Src.PWS.Interpreter
{
    public class MemberSlice
    {
        public static string[] computing = {"+", "-", "*" , "/", "%", ">", "<", ">=", "<=", "==", "?=" , ":=", "!=",
        "!", "&"};
        public static string[] is_struct = {">?", ">!"};
        // `: annotation @: comment %%: variable ##: function
        // the _ mean private 
        public Dictionary<string, VariableData> global_variable = new Dictionary<string, VariableData>();
        public Dictionary<string, FunctionData> function_list = new Dictionary<string, FunctionData>();
        public class MemberBaseData
        {
            public int member_index;
            public bool is_private = false;
            public string name;
            public string comment_name;
            public string comment_value;

        }
        public class FunctionData : MemberBaseData
        {
            public List<Lexer.Token> token_list = new List<Lexer.Token>();
            public List<string> formal_parameter_list = new List<string>();
        }
        public class VariableData : MemberBaseData
        {
            public string init_data;
        }
        public MemberSlice(List<Lexer.Token> tokens)
        {
            int member_index = 0;
            bool is_function = false;
            Dictionary<string, List<string>> tk = new Dictionary<string, List<string>>();
            FunctionData currentFunction = null;
            foreach(Lexer.Token i in tokens)
            {
                string temp_comment_name = "";
                string temp_comment_value = "";
                // Because I don't know when the this function end, so I decide to make two FunctionData
                // The one is load the last data, when find the end of the last one, load the old and create new data
                //
                FunctionData function_data = new FunctionData();
                // mean annotation
                if (i.context[0] == "`")
                {
                    is_function = false;
                    continue;
                }
                // mean comment
                if (i.context[0] == "@")
                {
                    if (i.context.Count != 3)
                    {
                        PWSInterpreter.addOutPut($"Building Error In {member_index}: Error named the global name");
                    }
                    temp_comment_name = i.context[1];
                    temp_comment_value = i.context[2];
                    is_function = false;
                    continue;
                }
                // mean global value
                if (i.context[0] == "%%")
                {
                    VariableData variable_data = new VariableData();
                    variable_data.member_index = member_index;
                    variable_data.name = i.context[1];
                    variable_data.init_data = i.context[2];
                    variable_data.is_private = (i.context[1][0] == '_' ) ? true : false;
                    variable_data.comment_name = temp_comment_name;
                    variable_data.comment_value = temp_comment_value;
                    variable_data.comment_name = "";
                    variable_data.comment_value = "";
                    if (!global_variable.ContainsKey(variable_data.name))
                    {
                        global_variable.Add(variable_data.name, variable_data);
                    }
                    else
                    {
                        PWSInterpreter.addOutPut($"Building Error In {member_index}: The same variable name");
                    }
                    is_function = false;
                    continue;
                }
                // mean function
                if (i.context[0] == "##")
                {
                    if (currentFunction != null && !string.IsNullOrEmpty(currentFunction.name))
                    {
                        if (!function_list.ContainsKey(currentFunction.name))
                        {
                            function_list.Add(currentFunction.name, currentFunction);
                        }
                        else
                        {
                            PWSInterpreter.addOutPut($"Building Error In {member_index}: The same function name");
                        }
                    }
                    currentFunction = new FunctionData();
                    currentFunction.member_index = member_index;
                    currentFunction.name = i.context[1];
                    currentFunction.is_private = !string.IsNullOrEmpty(currentFunction.name) && currentFunction.name[0] == '_';
                    currentFunction.comment_name = temp_comment_name;
                    currentFunction.comment_value = temp_comment_value;

                    temp_comment_name = "";
                    temp_comment_value = "";
                   
                    if (i.context.Count > 2)
                    {
                        for (int j = 2; j < i.context.Count; j++)
                        {
                            currentFunction.formal_parameter_list.Add(i.context[j]);
                        }
                    }

                    continue;
                }

                // Now you can add token in function
                if (is_function && currentFunction != null)
                {
                    currentFunction.token_list ??= new List<Lexer.Token>();
                    if (!currentFunction.token_list.Contains(i))
                    {
                        i.type = Lexer.TokenType.CallFunc;
                        if (i.context.Count == 1)
                        {
                            if (i.context[0] == "#;")
                                i.type = Lexer.TokenType.EndFunc;
                            
                        }
                        if (i.context.Count == 2 && is_struct.Contains(i.context[0]))
                            i.type = Lexer.TokenType.Struct;

                        if (i.context.Count == 4)
                        {
                            if (Lexer.computing.Contains(i.context[1]))
                                i.type = Lexer.TokenType.Calculate;
                            if (i.context[1] == "=")
                                i.type = Lexer.TokenType.Assign;  
                            if (i.context[1] == "->")
                                i.type = Lexer.TokenType.ForceAssign;
                        }
                        currentFunction.token_list.Add(i);
                    }
                        
                }
                is_function = true;

                member_index++;
                continue;
            }
            if (currentFunction != null && !string.IsNullOrEmpty(currentFunction.name))
            {
                if (!function_list.ContainsKey(currentFunction.name))
                {
                    function_list.Add(currentFunction.name, currentFunction);
                }
                else
                {
                    PWSInterpreter.addOutPut("Building Error: The same function name");
                }
            }
        }
        public Dictionary<string, VariableData> GetGlobalVariableList()
        {
            return global_variable;
        }
        public Dictionary<string, FunctionData> GetFunctionDataList()
        {
            return function_list;
        }
    }
}