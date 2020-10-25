using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NeodymiumDotNet.Optimizations
{
    internal static class ReflectionUtils
    {
        public static MethodInfo? GetMethod(this Type type, string name, BindingFlags bindingFlags, params Type[] argumentTypes)
            => type
                .GetMethods(bindingFlags)
                .FirstOrDefault(m => m.Name == name && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(argumentTypes));


        public static MethodInfo? GetMethod(this Type type, string name, BindingFlags bindingFlags, Type[] typeArgParamTypes, params Type[] argumentTypes)
            => type
                .GetMethods(bindingFlags)
                .Where(m => m.Name == name && m.GetGenericArguments().Length == typeArgParamTypes.Length)
                .Select(m => m.MakeGenericMethod(typeArgParamTypes))
                .FirstOrDefault(m => m.GetParameters().Select(p => p.ParameterType).SequenceEqual(argumentTypes));
    }
}
