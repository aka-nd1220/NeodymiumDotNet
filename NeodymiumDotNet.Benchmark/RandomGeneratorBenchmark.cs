using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using NeodymiumDotNet.Random;

namespace NeodymiumDotNet.Benchmark
{
    public class RandomGeneratorBenchmark
    {

        private const int Iterations = 10000;


        private double TestCore(RandomGenerator gen)
        {
            var x = 0.0;
            for(var i = 0 ; i < Iterations ; ++i)
                x = gen.NextFloat64();
            return x;
        }


        [Benchmark]
        public double TestLinearCongruential()
            => TestCore(_LinearCongruential);


        private readonly static RandomGenerator _LinearCongruential
            = new LinearCongruentialGenerator();


        [Benchmark]
        public double TestMersenneTwister()
            => TestCore(_LinearCongruential);


        private readonly static RandomGenerator _MersenneTwister
            = new MersenneTwisterGenerator(0);


        [Benchmark]
        public double TestXorShift32()
            => TestCore(_XorShift32);


        private readonly static RandomGenerator _XorShift32
            = new XorShift32Generator();


        [Benchmark]
        public double TestXorShift64()
            => TestCore(_XorShift64);


        private readonly static RandomGenerator _XorShift64
            = new XorShift32Generator();


        [Benchmark]
        public double TestXorShift96()
            => TestCore(_XorShift96);


        private readonly static RandomGenerator _XorShift96
            = new XorShift32Generator();


        [Benchmark]
        public double TestXorShift128()
            => TestCore(_XorShift128);


        private readonly static RandomGenerator _XorShift128
            = new XorShift32Generator();

    }
}
