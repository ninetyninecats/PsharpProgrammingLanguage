# P# version 0.4


P# is a programming language designed to get the best of both worlds between the simple syntax of python and the speed of C#

P# has an interpreter like python in case you want to use it for quick scripting, and can compile to CLR bytecode. I might possibly make its own compiler but if I do it will be called PFlat

Note that the two previous lines are assuming that the language is actually at its 1.0 release or higher 

# Syntax

P# has syntax like python with some C family mixed in, here are some examples:

To declare a variable named x you can use `var x;`
You can also assign it in-line like this: `var x = 5;`
To declare a constant variable you can use `let x = 5;`
Yes, let is for constants.
That is inferring the type, if you like you can also use static typing like: `var x : int;` or `let x : int = 5;`

To have an if statement you can write `if (x > 5) {code}`
or there are inline if statements which go like `if (x > 5) code`

For functions you can write `fun X(a : int, b : int, c : int) : int {code}`


# Errors

P# has many errors, here is a list of all of them