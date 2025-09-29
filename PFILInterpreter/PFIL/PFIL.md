# Instructions
Table of Instructions by byte:

| |0  |1|2|3|4|5|6|7|8|9|A|B|C|D|E     |F   |
|-|---|-|-|-|-|-|-|-|-|-|-|-|-|-|------|----|
|0|NOP| | | | | | | | | | | | | |ADDUL|ANDUL|
|1|   | | | | | | | | | | | | | |ADDL |ANDL |
|2|   | | | | | | | | | | | | | |ADDD |ORUL |
|3|   | | | | | | | | | | | | | |N/A  |ORL  |
|4|   | | | | | | | | | | | | | |SUBUL|XORUL|
|5|   | | | | | | | | | | | | | |SUBL |XORL |
|6|   | | | | | | | | | | | | | |SUBD |NOTUL|
|7|   | | | | | | | | | | | | | |N/A  |NOTL |
|8|   | | | | | | | | | | | | | |MULUL|SHLUL|
|9|   | | | | | | | | | | | | | |MULL |SHLL |
|A|   | | | | | | | | | | | | | |MULD |SHRUL|
|B|   | | | | | | | | | | | | | |N/A  |SHRL |
|C|   | | | | | | | | | | | | | |DIVUL|ROLUL|
|D|   | | | | | | | | | | | | | |DIVL |ROLL |
|E|   | | | | | | | | | | | | | |DIVD |RORUL|
|F|   | | | | | | | | | | | | | |N/A  |RORL |
# Instructions to add

SUB
MUL
DIV
NGT
AND
OR
XOR
NOT
SHL
SHR
ROL
ROR
JMP
JEZ
JNZ
EQ
LT
GT
IMM
POP
PUSHVAR
POPVAR
PUSHARG
POPARG
PUSHTHIS
POPTHIS
SWP
DUP
CALL
RET
NEW
NEWARR

# File format
All numbers are little endian  

Everything you will find in a PFIL bytecode file:  

Magic number: `0x600DBEEF`  

File version number: 8 bits, currently 1  
Software major version number: 8 bits  
Software minor version number: 8 bits  
File flags: 32 bits  
|Flag   |Description|Bit|
|:------|:----------|:--|
|Library|Should be set if the file is a library|`0x00000001`

Pointer to entry point: 32 bits  
List of pointers to classes: 32 bits each

## Class section format
Number of constants: 16 bits  
Constants: various sizes  
Flags
