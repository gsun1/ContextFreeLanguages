# ContextFreeLanguages
A CFL parsing library

CFG.cs
CFG - the context free grammar class.
    
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

PDA.cs
PDA - the pushdown automaton class. Currently only supports
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

examples.cs - some example grammars to show the usage of the PDA
