using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NeodymiumDotNet.Test
{
    public class ComplexTrait : IValueTrait<Complex>
    {
        public bool IsNumber() => true;

        public Complex Zero() => 0;

        public Complex One() => 1;


        public Complex UnaryPlus(Complex value) => value;

        public Complex UnaryNegate(Complex value) => -value;

        public Complex Complement(Complex value) => throw new NotSupportedException();

        public Complex Not(Complex value) => throw new NotSupportedException();

        public Complex Increment(Complex value) => throw new NotSupportedException();

        public Complex Decrement(Complex value) => throw new NotSupportedException();



        public Complex Add(Complex lhs, Complex rhs) => lhs + rhs;

        public Complex Subtract(Complex lhs, Complex rhs) => lhs - rhs;

        public Complex Multiply(Complex lhs, Complex rhs) => lhs * rhs;

        public Complex Divide(Complex lhs, Complex rhs) => lhs / rhs;

        public Complex Modulo(Complex lhs, Complex rhs) => throw new NotSupportedException();

        public Complex BitwiseAnd(Complex lhs, Complex rhs) => throw new NotSupportedException();

        public Complex BitwiseOr(Complex lhs, Complex rhs) => throw new NotSupportedException();

        public Complex BitwiseXor(Complex lhs, Complex rhs) => throw new NotSupportedException();


        public bool Equals(Complex lhs, Complex rhs) => lhs == rhs;

        public bool NotEquals(Complex lhs, Complex rhs) => lhs != rhs;

        public bool LessThan(Complex lhs, Complex rhs) => throw new NotSupportedException();

        public bool LessThanOrEquals(Complex lhs, Complex rhs) => throw new NotSupportedException();

        public bool GreaterThan(Complex lhs, Complex rhs) => throw new NotSupportedException();

        public bool GreaterThanOrEquals(Complex lhs, Complex rhs) => throw new NotSupportedException();

        
        public Complex ShiftLeft(Complex lhs, int rhs) => throw new NotSupportedException();

        public Complex ShiftRight(Complex lhs, int rhs) => throw new NotSupportedException();
        

        public Complex FromDouble(double value) => value;

        public Complex FromLong(long value) => value;

        public double ToDouble(Complex value) => throw new NotSupportedException();

        public long ToLong(Complex value) => throw new NotSupportedException();


    }
}
