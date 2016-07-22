// examples.cs - some example grammars to show the usage of the PDA

using System;
using System.Collections.Generic;
using CFL;

namespace examples{
    class tester{
        public static void Main(){
            Console.WriteLine("Basic Arithmetic:");
            string[] variables = new string[]{"Expression"};
            string[] terminals = new string[]{"number", "+", "-", "*", "/","(",")"};
            string start = "Expression";
            string[] r1 = new string[]{"Expression", "Expression", "+", "Expression"};
            string[] r2 = new string[]{"Expression", "Expression", "-", "Expression"};
            string[] r3 = new string[]{"Expression", "Expression", "*", "Expression"};
            string[] r4 = new string[]{"Expression", "Expression", "/", "Expression"};
            string[] r5 = new string[]{"Expression", "(", "Expression", ")"};
            string[] r6 = new string[]{"Expression", "number"};
            string[][] rules = new string[][]{r1,r2,r3,r4,r5,r6};
            CFG arith = new CFG(variables,terminals,start,rules);
            PDA arith_PDA = new PDA(arith);
            string[] input1 = new string[]{"number","+","number", "-", "number"};
            string[] input2 = new string[]{"number","+","(","number","-","number",")"};
            string[] input3 = new string[]{"number", "+"};
            string[] input4 = new string[]{"number","number","number","number","number","number"};
            string[][] inputs = new string[][]{input1,input2,input3,input4};
            arith.print_CFG();
            foreach(string[] input in inputs) {
                string str = "";
                foreach(string s in input) {
                    str += " " + s + " ";
                }
                bool it = arith_PDA.greedy_parse(input);
                Console.WriteLine("String to parse: {0}",str);
                Console.WriteLine("Accepted? {0}", it);
            }

            Console.WriteLine("\nLogic:");

            string[] lvariables = new string[]{"Expression","Variable"};
            string[] lterminals = new string[]{"->", "and", "or", "not", "a", "b", "c","(",")"};
            string lstart = "Expression";
            string[] lr1 = new string[]{"Expression","Variable"};
            string[] lr2 = new string[]{"Expression", "(","Expression",")"};
            string[] lr3 = new string[]{"Variable","a"};
            string[] lr4 = new string[]{"Variable","b"};
            string[] lr5 = new string[]{"Variable","c"};
            string[] lr6 = new string[]{"Expression","not","Expression"};
            string[] lr7 = new string[]{"Expression","Expression","and","Expression"};
            string[] lr8 = new string[]{"Expression","Expression","or","Expression"};
            string[] lr9 = new string[]{"Expression","Expression","->","Expression"};
            string[][] lrules = new string[][]{lr1,lr2,lr3,lr4,lr5,lr6,lr7,lr8,lr9};
            CFG logic = new CFG(lvariables,lterminals,lstart,lrules);
            PDA logic_PDA = new PDA(logic);
            string[] linput1 = new string[]{"(", "a", "->", "not", "b",")", "and", "c"};
            string[][] linputs = new string[][]{linput1};
            logic.print_CFG();
            foreach(string[] input in linputs) {
                string str = "";
                foreach(string s in input) {
                    str += " " + s + " ";
                }
                bool it = logic_PDA.greedy_parse(input);
                Console.WriteLine("String to parse: {0}",str);
                Console.WriteLine("Accepted? {0}", it);
            }
        }
    }
}