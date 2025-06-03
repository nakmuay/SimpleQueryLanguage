lexer grammar MathLexer;

LEFT_PARENTHESIS  : '(' ;
RIGHT_PARENTHESIS : ')' ;

OP_ADD : '+' ;
OP_SUB : '-' ;
OP_MUL : '*' ;
OP_DIV : '/' ;
OP_POW : '^' ;

EQ     : '=' ;

NUM    : [0-9]+ ('.' [0-9]+)? ([eE] [+-]? [0-9]+)? ;
ID     : [a-zA-Z] ;
WS     : [ \t\r\n] -> channel(HIDDEN) ;

UNARY_FN_COS : 'cos' ;
UNARY_FN_SIN : 'sin' ;
UNARY_FN_ARCCOS : 'arccos' ;
UNARY_FN_ARCSIN : 'arcsin' ;