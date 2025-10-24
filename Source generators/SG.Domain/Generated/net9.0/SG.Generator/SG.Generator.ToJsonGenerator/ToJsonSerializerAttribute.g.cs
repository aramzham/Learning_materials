using System;

namespace SG.Generator;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ToJsonSerializerAttribute : Attribute
{
    public bool Minified { get; }
    public Type TargetType { get; }
    
    public ToJsonSerializerAttribute(Type targetType, bool minified = false)
    {
        Minified = minified;
        TargetType = targetType;
    }
}