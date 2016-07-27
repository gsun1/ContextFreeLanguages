// Token.cs - implementation of Token class for lex-style tokenizer

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Tokenizer;


namespace Tokenizer {

    /*
    Token<T> - Tokens associated with type T
    State:
    Name - a string to denote the name of the token
    Value - the value associated with the token

    Methods:
    init(Name,Value) - manually initialize token
    init() - a dummy initializer method for testing purposes
    printToken() - prints token in the format (Name, Value)
    */
    public class Token<T> {
        public string Name { get; }
        public T Value { get; }

        public Token(string Name, T Value) {
            this.Name = Name;
            this.Value = Value;
        }

        public Token() {}

        public void printToken() {
            Console.WriteLine("(\"{0}\", {1})", Name, Value);
        }
    }

    // For testing. Allows you to run file by itself through compiler to check for compilation errors
    /*class test {
        public static void Main() {
            RegExRule<int> r1 = new RegExRule<int>("[0-9]*",(str => Convert.ToInt32(str)));
            RegExRule<int> r2 = new RegExRule<int>("\\s",(str => 0));
            RegExRule<int>[] rules = new RegExRule<int>[] {r1, r2};
            Tokenizer<int> t = new Tokenizer<int>(rules);
            List<Token<int>> tokens = t.tokenize("1324 223 3");
            foreach(Token<int> token in tokens) {
                token.printToken();
            }
        }
    }*/
}