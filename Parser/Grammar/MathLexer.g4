lexer grammar MathLexer;

LEFT_PARENTHESIS: '(';
RIGHT_PARENTHESIS: ')';

OP_ADD: '+';
OP_SUB: '-';
OP_MUL: '*';
OP_DIV: '/';
OP_POW: '^';

EQ: '=';

NUM : [0-9]+ ('.' [0-9]+)? ([eE] [+-]? [0-9]+)?;
ID  : [a-zA-Z]+;
WS  : [ \t\r\n] -> channel(HIDDEN);