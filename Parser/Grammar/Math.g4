grammar Math;

equation
    : left=expr eq='=' right=expr EOF
    ;

expr
    : '(' expr ')'                         # parensExpr
    | op=('+'|'-') expr                    # unaryExpr
    | left=expr op='^' right=expr          # binaryExpr
    | left=expr op=('*'|'/') right=expr    # binaryExpr
    | left=expr op=('+'|'-') right=expr    # binaryExpr
    | func=ID '(' expr ')'                 # funcExpr
    | value=NUM                            # numberExpr
    | coeff=NUM var=ID                     # variableExpr
    | var=ID                               # variableExpr
    ;

OP_ADD: '+';
OP_SUB: '-';
OP_MUL: '*';
OP_DIV: '/';
OP_POW: '^';

EQ: '=';

NUM : [0-9]+ ('.' [0-9]+)? ([eE] [+-]? [0-9]+)?;
ID  : [a-zA-Z]+;
WS  : [ \t\r\n] -> channel(HIDDEN);