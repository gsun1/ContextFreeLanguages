// PDA.cs - pushdown automaton implementation for parsing CFL's

using System;
using System.Collections.Generic;
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
    stack_init() - creates an empty stack and sets its depth to 0
    push(s) - pushes s to top of the stack
    pop() - returns top of the stack
    peak(n) - returns a list of the top n elements of the stack from bottom to top
    CNFparse(input) - an initial attempt at parsing CNF grammars. Currently untested
    greedy_parse(input) - parses input by attempting to reduce variables into expressions
    at earliest possible time.
    */
    public class PDA {
        List<string> stack;
        int stack_depth;
        CFG grammar;

        public PDA(CFG grammar) {
            this.grammar = grammar;
        }

        private void stack_init() {
            stack = new List<string>{};
            stack_depth = 0;
        }

        private void push(string s) {
            stack.Add(s);
            stack_depth += 1;
        }

        private string pop() {
            if (stack_depth > 0) {
                string result = stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
                stack_depth -= 1;
                return result;
            }
            else {
                return "Invalid";
            }
        }

        private string[] peak(int n) {
            if (stack_depth >= n) {
                string[] result = new string[n];
                for(int i = 0; i < n; ++ i) {
                    result[i] = stack[stack_depth - n + i];
                }
                return result;
            }
            else {
                return new string[] {};
            }
        }

        public bool CNFparse(string[] input) {
            stack_init();
            string inverse;
            foreach(string s in input) {
                string[] l = new string[] {s};
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
        }
        public bool greedy_parse(string[] input) {
            stack_init();
            string inverse;
            // read each token in order, one at a time.
            foreach(string s in input) {
                push(s);
                //For testing
                /*Console.WriteLine("Stack:");
                foreach(string str in stack) {
                    Console.WriteLine(str);
                }*/
                // while it might still be possible to reduce
                while (true) {
                    bool change_made = false; // tracks is a reduction happens
                    // loop through all possible products
                    foreach (string[] product in grammar.Products) {
                        inverse = grammar.inverse(peak(product.Length));
                        // if this rule exists
                        if (inverse != "") {
                            for (int i = 0; i < product.Length; ++i) {
                                pop();
                            }
                            push(inverse);
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
                    /*else {
                        Console.WriteLine("Stack:");
                        foreach(string str in stack) {
                            Console.WriteLine(str);
                        }
                    }*/
                }
            }
            if (stack_depth == 1 && stack[0] == grammar.Start) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}