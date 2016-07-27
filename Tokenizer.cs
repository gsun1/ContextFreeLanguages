// Tokenizer.cs - a simple lex style tokenizer for C#

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Tokenizer;

namespace Tokenizer{

    /*
    Tokenizer - A simple tokenizer to turn input strings into tokens based on user-defined rules

    State:
    Rules - An array of rules to be applied to the input string

    Methods:
    init(Rules) - sets the Rules filed to the input
    tokenize(input) - takes in a string and returns a list of tokens by applying Rules

    Notes: this tokenizer skips whitespaces, so tokens should be defined to not include
    whitespace.
    Precedence for tokenizing follows the same rules as lex:
        1. Longer rules take precedence over shorter rules
        2. In cases of ties, earlier rules take precedence over later rules
    */
    class Tokenizer<T> {
        public RegExRule<T>[] Rules { get; }

        public Tokenizer(RegExRule<T>[] Rules) {
            this.Rules = Rules;
        }

        public List<Token<T>> tokenize(string input) {
            int position = 0; // only start parsing from where we left off
            int input_length = input.Length;
            List<Token<T>> result = new List<Token<T>>{};
            while (position != input_length) {
                // skip spaces
                if(Char.IsWhiteSpace(input[position])) {
                    ++position;
                }
                else {
                    string candidate = "";
                    RegExRule<T> candidate_rule = new RegExRule<T>();
                    int maxLength = 0; // the longest rule takes precedence
                    foreach (RegExRule<T> r in Rules) {
                        Match m = r.R.Match(input,position);
                        if (m.Length > maxLength && m.Index == position) {
                            candidate = m.Value;
                            candidate_rule = r;
                            maxLength = m.Length;
                        }
                    }
                    // Temporary solution, so I don't have to
                    // worry about throwing exceptions
                    if (maxLength == 0) {
                        Console.WriteLine("Entire input not matched");
                        break;
                    }
                    else {
                        Token<T> t = new Token<T>(candidate_rule.Name(candidate), candidate_rule.Op(candidate));
                        result.Add(t);
                        position += maxLength;
                    }
                }
            }
            return result;
        }
    }
}
