Modules are a grouping of F# values and types.
Typically grouped together as they work on the same data type(s) (f.e. like List module has a lot of functionality related to lists).


Modules are represented as static classes in .net.


Namespaces are useful to avoid type name collisions.


Order of the files in compilation is important in F#.


Access control

Access specifier | Effective access
-----------------------------------
public           | access by all callers (default)
private          | only accessible from within the enclosing type or module
internal         | only accessible from within the assembly
