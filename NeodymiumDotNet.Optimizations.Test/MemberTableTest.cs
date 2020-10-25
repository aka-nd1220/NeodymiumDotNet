using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace NeodymiumDotNet.Optimizations.Test
{
    public class MemberTableTest
    {
        [Fact]
        public void MemoryMarshal()
        {
           var castForSpan = MemberTable._MemoryMarshal.Cast<uint, float>.ForSpan;
           Assert.Equal(typeof(MemoryMarshal), castForSpan.DeclaringType);
           Assert.Equal(typeof(Span<uint>), castForSpan.GetParameters()[0].ParameterType);
           Assert.Equal(typeof(Span<float>), castForSpan.ReturnType);

            var castForReadOnlySpan = MemberTable._MemoryMarshal.Cast<uint, float>.ForReadOnlySpan;
            Assert.Equal(typeof(MemoryMarshal), castForReadOnlySpan.DeclaringType);
            Assert.Equal(typeof(ReadOnlySpan<uint>), castForReadOnlySpan.GetParameters()[0].ParameterType);
            Assert.Equal(typeof(ReadOnlySpan<float>), castForReadOnlySpan.ReturnType);
        }
    }
}
