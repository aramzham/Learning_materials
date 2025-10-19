using System;

namespace SG.Generator;

[AttributeUsage(AttributeTargets.Class)]
public class ToJsonSerializerAttribute : Attribute
{
    public bool Minified { get; }
    
    public ToJsonSerializerAttribute(bool minified = false)
    {
        Minified = minified;
    }
}