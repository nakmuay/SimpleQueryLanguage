#!bin/bash
java -jar ./Docker/antlr-4.13.0-complete.jar -Dlanguage=CSharp ../Parser/Grammar/*.g4 -visitor
