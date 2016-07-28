// PDA.cs - pushdown automaton implementation for parsing CFL's

using System;
using System.Collections.Generic;
using System.Linq;
using Tokenizer;
using PE;
using CFL;

namespace CFL {
    /* PDA - the pushdown automaton class. Currently only supports
    greedy implementation for deterministic CFGs
    State:
    stack - implementation of a stack using the List generic. This is
    done because the default stack implementation does not allow us to
    peak more than one character down.
    stack_depth - size of the stack. Used to maintain stack abstraction
    grammar - the CFG associated with this particular CFG.

    Methods:
    stack_init() - creates an empty stack and sets its depth to 0.
    CNFparse(input) - an initial attempt at parsing CNF grammars. Currently untested.
    greedy_parse(input) - parses input by attempting to reduce variables into expressions
    at earliest possible time.

    Private Methods:
    push(s) - pushes s to top of the stack.
    pop() - returns top of the stack.
    peak(n) - returns a list of the string components of the top n elements of the stack from bottom to top.

    Notes:
    The weird "ruleset" vs. "rule" names are an accident of a previous implementation that only returned whether or
    not the input is accepted or denied. Future revisions might replace the name to be more readable
    */
    public class PDA<T> {
        List<Token<T>> stack;
        int stack_depth;
        CFG<T> grammar;

        public PDA(CFG<T> grammar) {
            this.grammar = grammar;
        }

        private void stack_init() {
            stack = new List<Token<T>>{};
            stack_depth = 0;
        }

        private void push(Token<T> t) {
            stack.Add(t);
            stack_depth += 1;
        }

        private Token<T> pop() {
            if (stack_depth > 0) {
                Token<T> result = stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
                stack_depth -= 1;
                return result;
            }
            else {
                // just for testing purposes
                Token<T> result = new Token<T>();
                return result;
            }
        }

        private string[] peak(int n) {
            if (stack_depth >= n) {
                string[] result = new string[n];
                for(int i = 0; i < n; ++ i) {
                    result[i] = stack[stack_depth - n + i].Name;
                }
                return result;
            }
            else {
                return new string[] {};
            }
        }

        /*public bool CNFparse(Token<T>[] input) {
            stack_init();
            string inverse;
            foreach(Token<T> t in input) {
                string[] l = new string[] {t};
                inverse = grammar.inverse(l);
                if (inverse == "") {
                    return false;
                }
                else {
                    push(inverse);
                }
            }
            while (stack_depth > 1) {
                inverse = grammar.inverse(peak(2));
                if (inverse == "") {
                    Console.WriteLine("Failed on reduce");
                    foreach(string s in stack){
                        Console.WriteLine(s);
                    }
                    return false;
                }
                else {
                    pop();
                    pop();
                    push(inverse);
                }
            }
            if (stack_depth == 1 && stack[0] == grammar.Start) {
                return true;
            }
            else {
                return false;
            }
        }*/

        public T greedy_parse(Token<T>[] input) {
            stack_init();
            string inverse;
            // read each token in order, one at a time.
            foreach(Token<T> t in input) {
                push(t);
                //For testing
                /*Console.WriteLine("Stack:");
                foreach(Token<T> tok in stack) {
                    Console.WriteLine(tok.Name);
                }*/
                // while it might still be possible to reduce
                while (true) {
                    bool change_made = false; // tracks if a reduction happens
                    // loop through all possible products
                    foreach (string[] product in grammar.Products) {
                        string[] p = peak(product.Length);
                        // first make sure the rule matches the top of the stack
                        bool match = p.Length == product.Length;
                        if(match) {
                            for (int i = 0; i < p.Length; ++i) {
                                if (p[i] != product[i]) {
                                    match = false;
                                    break;
                                }
                            }
                        }
                        if (match){
                            inverse = grammar.inverse(peak(product.Length));
                            //Console.WriteLine("Inverse:");
                            //Console.WriteLine(inverse);
                            string[] rule = new string[]{inverse};
                            T value = default(T);
                            rule = rule.Concat(product).ToArray();
                            // if this rule exists
                            /*foreach (string str in product) {
                                Console.Write(str);
                            }
                            Console.WriteLine();*/
                            int plength = product.Length; // length of the product to be reduced
                            T[] tvalues = new T[plength + 1]; // array of values the tokens take on
                            tvalues[0] = default(T); // rules should not involve the LHS, hence it does
                            //not matter what goes here.
                            for (int i = 1; i <= plength; ++i) {
                                tvalues[i] = stack[stack_depth - plength + i - 1].Value;
                            }
                            for (int i = 0; i < plength; ++i) {
                                pop();
                            }
                            foreach (Tuple<string[],Func<T[],T>> ruleset in grammar.Rules) {
                                if (Enumerable.SequenceEqual(rule,ruleset.Item1)) {
                                    value = ruleset.Item2(tvalues); // set value of new token based on Rule
                                }
                            }
                            Token<T> tmp = new Token<T>(inverse,value);
                            push(tmp);
                            //Console.WriteLine(value);
                            change_made = true; // track that a reduction happened
                            break;
                        }
                    }
                    // break while loop only when the entire list of products is
                    // tried and no reduction is possible.
                    if (!change_made) {
                        break;
                    }
                    // for testing
                    else {
                        /*Console.WriteLine("Stack:");
                        foreach(Token<T> tok in stack) {
                            Console.WriteLine(tok.Name);
                        }*/
                    }
                }
            }
            if (stack_depth == 1 && stack[0].Name == grammar.Start) {
                return stack[0].Value;
            }
            else {
                throw new ParseException("Expression could not be fully reduced");
            }
        }
    }

    /*public class tester {
        public static void Main() {
            Console.WriteLine("Hello World!");
        }
    }*/
}
