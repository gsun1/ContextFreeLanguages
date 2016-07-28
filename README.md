# ContextFreeLanguages
A CFL parsing library

RegExRule.cs - a class to allow the easy creation of tokens from regular expressions
RegExRule<T> - a class for creating rules for turning regular expressions into tokens associated with type T.
  State:
  R - the regular expression to be tokenized
  Name - a function that maps from the regular expression to a desired token name
  Op - a function that maps from the regular expression to an assigned value

  Methods:
  init(expression,Name,Op) - initializer to manually assign each of the fields
  init() - dummy initializer for testing purposes

Token.cs - implementation of Token class for lex-style tokenizer
Token<T> - Tokens associated with type T
  State:
  Name - a string to denote the name of the token
  Value - the value associated with the token

  Methods:
  init(Name,Value) - manually initialize token
  init() - a dummy initializer method for testing purposes
  printToken() - prints token in the format (Name, Value)

Tokenizer.cs - a simple lex style tokenizer for C#
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

CFG.cs
CFG<T> - the context free grammar class of type T
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

examples.cs - Some example grammars to show the usage of the PDA



For the future:
The parser currently used uses a greedy application rule which works for single precedence, right associative grammars. A "look-ahead" functionality will need to be added in order to make the grammar robust enough for most purposes.
