Functions always return a value.
Return unit, return Success or Fail, return the input value to create pipelines.


Function shall not look up a data to calculate the result.
Lookup must be done by the function caller and pass that data as a parameter to the function.


Functions shall not change data outside of their scope.


Infix functions has 2 arguments - one to its left, one to its right.
1 + 3


F# is left associative regarding related to applying arguments to function parameters.
square 3 + 1 = ???
distance 2 7 * 2 = ???


Currying is the conversion of a function that takes multiple arguments into a sequence of functions each taking a single argument.


1. A recursive function must know where to stop = Base case.
2. Change state and get closer to base case.
3. Calls itself.
