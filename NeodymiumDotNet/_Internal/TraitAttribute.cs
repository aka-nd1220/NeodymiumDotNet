using System;
using System.Collections.Generic;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Means that the interface is trait definition type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    internal class TraitAttribute : Attribute
    {

    }

    /// <summary>
    ///     Means that the interface member is trait definition.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method
                    | AttributeTargets.Property)]
    internal class TraitMemberAttribute : Attribute
    {

        public string? MappedName { get; }


        public  TraitMemberAttribute(string? mappedName = null)
            => MappedName = mappedName;

    }
}
