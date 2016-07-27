// RegExRule.cs - a class to allow the easy creation of tokens from regular expressions



using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Tokenizer{

    /*
    RegExRule<T> - a class for creating rules for turning regular expressions into tokens associated with type T.
    
    State:
    R - the regular expression to be tokenized
    Name - a function that maps from the regular expression to a desired token name
    Op - a function that maps from the regular expression to an assigned value

    Methods:
    init(expression,Name,Op) - initializer to manually assign each of the fields
    init() - dummy initializer for testing purposes
    */
    class RegExRule<T> {
        public Regex R { get; }
        public Func<string,string> Name { get; }
        public Func<string,T> Op { get; }

        public RegExRule(string expression, Func<string,string> Name, Func<string,T> Op) {
            R = new Regex(expression);
            this.Op = Op;
            this.Name = Name;
        }
        public RegExRule() {
        }
    }
}