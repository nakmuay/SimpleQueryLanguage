parser grammar MathParser;

options { tokenVocab = MathLexer; }

equation
    : left=expr eq=EQ right=expr EOF
    ;

expr
    : LEFT_PARENTHESIS expr RIGHT_PARENTHESIS   # parensExpr
    | op=(OP_ADD|OP_SUB) expr                   # unaryExpr
    | left=expr op=OP_POW right=expr            # binaryExpr
    | left=expr op=(OP_MUL|OP_DIV) right=expr   # binaryExpr
    | left=expr op=(OP_ADD|OP_SUB) right=expr   # binaryExpr
    | func=ID '(' expr ')'                      # funcExpr
    | value=NUM                                 # constantExpr
    | coeff=NUM var=ID                          # variableExpr
    | var=ID                                    # variableExpr
    ;