// examples.cs - some example grammars to show the usage of the PDA

using System;
using System.Collections.Generic;
using CFL;
using Tokenizer;

namespace examples{
    class tester{
        public static void Main(){
            Console.WriteLine("Integer Arithmetic:");
            string[] variables = new string[]{"Expression"};
            string[] terminals = new string[]{"number", "+", "-", "*", "/","(",")"};
            string start = "Expression";
            string[] re1 = new string[]{"Expression", "Expression", "+", "Expression"};
            Tuple<string[],Func<int[],int>> r1 = 
                new Tuple<string[],Func<int[],int>>(re1, ( x => x[1] + x[3]));
            string[] re2 = new string[]{"Expression", "Expression", "-", "Expression"};
            Tuple<string[],Func<int[],int>> r2 = 
                new Tuple<string[],Func<int[],int>>(re2, ( x => x[1] - x[3]));
            string[] re3 = new string[]{"Expression", "Expression", "*", "Expression"};
            Tuple<string[],Func<int[],int>> r3 = 
                new Tuple<string[],Func<int[],int>>(re3, ( x => x[1] * x[3]));
            string[] re4 = new string[]{"Expression", "Expression", "/", "Expression"};
            Tuple<string[],Func<int[],int>> r4 = 
                new Tuple<string[],Func<int[],int>>(re4, ( x => x[1] / x[3]));
            string[] re5 = new string[]{"Expression", "(", "Expression", ")"};
            Tuple<string[],Func<int[],int>> r5 = 
                new Tuple<string[],Func<int[],int>>(re5, ( x => x[2] ));
            string[] re6 = new string[]{"Expression", "number"};
            Tuple<string[],Func<int[],int>> r6 = 
                new Tuple<string[],Func<int[],int>>(re6, ( x => x[1] ));

            Tuple<string[],Func<int[],int>>[] rules = new Tuple<string[],Func<int[],int>>[]{r1,r2,r3,r4,r5,r6};
            CFG<int> arith = new CFG<int>(variables,terminals,start,rules);
            arith.print_CFG();

            RegExRule<int> rer1 = new RegExRule<int>("[0-9]*",(str => "number"),(str => Convert.ToInt32(str)));
            RegExRule<int> rer2 = new RegExRule<int>("[+\\-*/()]",(str => str),(str => 0));
            RegExRule<int>[] rerules = new RegExRule<int>[] {rer1, rer2};
            Tokenizer<int> t = new Tokenizer<int>(rerules);
            
            string exp1 = "6*(12+3)";

            string exp2 = "1 + (2 * (5 + (4 - 3)))";

            string exp3 = "1 + ";

            string[] expressions = new string[] {exp1,exp2,exp3};

            foreach (string exp in expressions){ 
                Console.WriteLine("Expression to parse:\n{0}\n",exp);
                List<Token<int>> tokens = t.tokenize(exp);
                /*foreach(Token<int> token in tokens) {
                    token.printToken();
                }*/
                Console.WriteLine("Expression evaluates to:");
                PDA<int> arith_PDA = new PDA<int>(arith);
                int answer = arith_PDA.greedy_parse(tokens.ToArray());
                if (answer != 0) {
                    Console.WriteLine(answer);
                }
                else {
                    Console.WriteLine("Invalid expression");
                }
            }





            /*string[] input1 = new string[]{"number","+","number", "-", "number"};
            string[] input2 = new string[]{"number","+","(","number","-","number",")"};
            string[] input3 = new string[]{"number", "+"};
            string[] input4 = new string[]{"number","number","number","number","number","number"};
            string[][] inputs = new string[][]{input1,input2,input3,input4};
            foreach(string[] input in inputs) {
                string str = "";
                foreach(string s in input) {
                    str += " " + s + " ";
                }
                bool it = arith_PDA.greedy_parse(input);
                Console.WriteLine("String to parse: {0}",str);
                Console.WriteLine("Accepted? {0}", it);
            }*/

            /*Console.WriteLine("\nLogic:");

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
            }*/
        }
    }
}