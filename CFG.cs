// CFL.cs - classes for context free grammars

using System;
using System.Collections.Generic;

namespace CFL {

    /*
    CFGException - exception for when the CFG cannot be initialized based on
    the parameters
    */

    public class CFGException : Exception {
        public CFGException() {}
        public CFGException(string message) : base(message) {}
    }

    /* CFG<T> - the context free grammar class of type T
    State:
    Variables - The variables of the context free grammar. 
    Terminals - The terminals of the context free grammar.
    Start - The start variable. It must be a variable.
    Rules - The rules of the context free grammar, in the form of a tuple. The
    first item in the tuple is a string array representation of a rule where the
    first element of the array is the LHS of the rule and the subsequet strings
    are the RHS. The second element is a function from an array of type T to type T.
    It essentially uses the value of the tokens parsed to determine a value for the
    token that is reduced to.
    Products - A list of the right hand sides of all of the rules
    MaxProduct - The length of the longest product

    Methods:
    init(V,T,S,R) - Initializes a context free grammar with variables V,
    terminals T, start variable S, and rules R
    inverse - Takes a list of strings and checks if the list matches the
    right hand side of any of the rules. If it does, it returns the left hand
    side. Otherwise, it returns the empty string.
    print_CFG - Prints the context free grammar in a pritty format
    */
    public class CFG<T> {
        public string[] Variables { get; }
        public string[] Terminals { get; }
        public string Start { get; }
        public Tuple<string[],Func<T[],T>>[] Rules { get; }
        public string[][] Products { get; }
        public int MaxProduct { get; }
        public CFG(string[] V, string[] T, string S, Tuple<string[],Func<T[],T>>[] R) {
            bool test1 = true; // make sure no Variables are also Terminals.
            bool test2 = false; // make sure the start variable is in Variables.
            bool test3 = true; // make sure rules only have variables on the left
            //and variables or terminals on the right.
            foreach (string variable in V) {
                if (variable == S) {
                    test2 = true;
                    break;
                }
                foreach (string terminal in T) {
                    if (variable == terminal) {
                        test1 = false;
                    }
                }
            }
            foreach (Tuple<string[],Func<T[],T>> ruleset in R) {
                string[] rule = ruleset.Item1;
                bool subtest = false;
                foreach (string variable in V) {
                    if (variable == rule[0]) {
                        subtest = true;
                        break;
                    }
                }
                if (!subtest) {
                    test3 = false;
                    break;
                }
                for (int i = 1; i < rule.Length; ++i) {
                    foreach (string terminal in T) {
                        if (terminal == rule[i]) {
                            subtest = true;
                            break;
                        }
                    }
                    if (subtest) {
                        break;
                    }
                    foreach (string variable in V) {
                        if (variable == rule[i]){
                            subtest = true;
                            break;
                        }
                    }
                    if (!subtest) {
                        test3 = false;
                    }
                }
            }
            //all tests passed
            if (test1 && test2 && test3) {
                Variables = V;
                Terminals = T;
                Start = S;
                Rules = R;
                // build products based on the rules.
                List<string[]> builder = new List<string[]>{};
                foreach (Tuple<string[],Func<T[],T>> ruleset in Rules) {
                    string[] rule = ruleset.Item1;
                    string[] tmp = new string[rule.Length - 1];
                    for (int i = 1; i < rule.Length; ++i) {
                        tmp[i-1] = rule[i];
                    }
                    builder.Add(tmp);
                }
                Products = builder.ToArray();
                // derive the biggest product from the list of products
                MaxProduct = 0;
                foreach (string[] product in Products) {
                    if (product.Length > MaxProduct) {
                        MaxProduct = product.Length;
                    }
               }
            }
            else {
                throw new CFGException("CFG not properly specified. Initiation failed.");
            }
        }
        public string inverse(string[] right) {
            foreach (Tuple<string[],Func<T[],T>> ruleset in Rules) {
                string[] rule = ruleset.Item1;
                /*foreach (string str in rule) {
                    Console.Write(str);
                }
                Console.WriteLine();*/
                // only try to match if the RHS matches the input in length
                if (right.Length == rule.Length - 1) {
                    bool match = true;
                    for (int i = 0; i < right.Length; ++i) {
                        // something doesn't match. This can't be right
                        if (right[i] != rule[i + 1]) {
                            match = false;
                            break;
                        }
                    }
                    if (match) {
                        return rule[0];
                    }
                }
            }
            return "";
        }
        public void print_CFG() {
            // Print variables
            Console.WriteLine("Variables:");
            foreach(string v in Variables){
                Console.WriteLine(v);
            }
            Console.WriteLine();
            // Print terminals
            Console.WriteLine("Terminals:");
            foreach(string t in Terminals) {
                Console.WriteLine(t);
            }
            Console.WriteLine();
            //Print Start
            Console.WriteLine("Start: {0}", Start);
            Console.WriteLine();
            // Print Rules
            Console.WriteLine("Rules:");
            foreach(Tuple<string[],Func<T[],T>> ruleset in Rules){
                string[] rule = ruleset.Item1;
                string s = rule[0] + " -> ";
                for(int i = 1; i < rule.Length; ++i) {
                    s += " " + rule[i] + " ";
                }
                Console.WriteLine(s);
            }
            Console.WriteLine();
            /*Console.WriteLine("Products:");
            foreach(string[] inv in Products) {
                foreach (string str in inv) {
                    Console.Write(str);
                }
                Console.WriteLine();
            }*/
        }
    }

    /*public class tester {
        public static void Main() {
            Console.WriteLine("Hello World!");
        }
    }*/
}
