Lists in F# are ordered, immutable series of homogeneous elements which can share common base type.

- implemented via a single linked list => ```O(n)``` time for the lookup.
- are immutable
	- elements cannot be changed
	- size cannot be changed
- supports slicing


Cons ```(::)``` makes a new list with a new first element.
Concat ```(@)``` makes a new list with the two lists combined. 


Sequences are logical series of elements all of one type.
Values are computed as required (think of ```yield``` in ```C#```) and are consumed one at a time.
Functions that accept sequences can accept lists, arrays etc. but not vice versa.


Arrays are fixed size at time of allocation.
Items must be of same type or implicitly convertible.
Array represent a contiguous block of memory (unlike lists which are linked lists).
Items in array may be changed with <- operator.


Immutable dictionaries in F# are implementation of ```IDictionary<K,V>```.
All keys and values are specified at initialization, after that no modification is allowed.
These are great for holding static / reference data.

To use a mutable dictionary, use .Net's ```System.Collections.Generic.Dictionary``` class.