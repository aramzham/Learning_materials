### Types in F#

Paranthesis are reserved for tuples => functions don't use it


The general rule of thumb with tuples is up to 5-6 items.


Records are simple aggregates of named values.
They are immutable.
Requires a type definition.


You need to initialize all the memebers of the record otherwise a compiler error will be raised.


If you need to make a copy of a record => use with keyword, which will copy all the fields from initial record and change fields specified in __with__


Option.map will execute the provided function if the input has a value and if not => will not do anything.
Option.bind expects a function that will return an option.


>>= is a shortcut
let (>>=) result func = Result.bind func result


Even though F# is a functional language it has a full support for OOP and classes (methods, props, events).

You have to define types for fields in classes.
```
type A = class
	val B : float
```
this is called an explicit field. They are immutable by default.

While classes give you a great compatibility with .net framework, but you can do better everything with F# types.


To use the method from interface you must upcast the object to interface first using :> operator.
```
(x1 :> IPrintable).Print()
```