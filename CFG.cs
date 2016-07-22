// CFL.cs - classes for context free grammars

using System;
using System.Collections.Generic;

namespace CFL {

    /* CFG - the context free grammar class.
    State:
    Variables - The variables of the context free grammar. 
    Terminals - The terminals of the context free grammar.
    Start - The start variable. It must be a variable.
    Rules - The rules of the context free grammar. It must begin with
    a variable and end with variables and terminals
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
    public class CFG {
        public string[] Variables { get; }
        public string[] Terminals { get; }
        public string Start { get; }
        public string[][] Rules { get; }
        public string[][] Products { get; }
        public int MaxProduct { get; }
        public CFG(string[] V, string[] T, string S, string[][] R) {
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
            foreach (string[] rule in R) {
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
                foreach (string[] rule in Rules) {
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
            //temporary solution. Will throw exception in the future
            else {
                Console.WriteLine("CFG not properly specified. Initiation failed.");
            }
        }
        public string inverse(string[] right) {
            foreach (string[] rule in Rules) {
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
            foreach(string[] rule in Rules){
                string s = rule[0] + " -> ";
                for(int i = 1; i < rule.Length; ++i) {
                    s += " " + rule[i] + " ";
                }
                Console.WriteLine(s);
            }
            Console.WriteLine();
        }
    }
}