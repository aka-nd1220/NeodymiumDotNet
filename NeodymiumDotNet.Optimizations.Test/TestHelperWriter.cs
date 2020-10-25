using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NeodymiumDotNet.Optimizations.Test
{
    internal sealed class TestHelperWriter : System.IO.TextWriter
    {
        private readonly ITestOutputHelper _helper;
        private StringBuilder _buffer;
        public override Encoding Encoding => Encoding.UTF8;

        public TestHelperWriter(ITestOutputHelper helper)
        {
            _helper = helper;
            _buffer = new StringBuilder();
        }

        public override void Write(char value)
        {
            if(value == '\n')
            {
                _helper.WriteLine(_buffer.ToString());
                _buffer = new StringBuilder();
            }
            else
            {
                _buffer.Append(value);
            }
        }

        public override void WriteLine(string message)
            => _helper.WriteLine(message);

        public override void WriteLine(string message, params object[] args)
            => _helper.WriteLine(message, args);
    }
}
