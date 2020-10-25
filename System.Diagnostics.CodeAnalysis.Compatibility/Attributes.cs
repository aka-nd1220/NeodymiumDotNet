using System;
using System.Collections.Generic;
using System.Text;

namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
    public sealed class AllowNullAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
    public sealed class DisallowNullAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
    public sealed class MaybeNullAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class MaybeNullWhenAttribute : Attribute
    {
        public bool ReturnValue { get; }
        public MaybeNullWhenAttribute(bool returnValue)
            => ReturnValue = returnValue;
    }


    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
    public sealed class NotNullAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
    public sealed class NotNullIfNotNullAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class NotNullWhenAttribute : Attribute
    {
        public bool ReturnValue { get; }
        public NotNullWhenAttribute(bool returnValue)
            => ReturnValue = returnValue;
    }
}
