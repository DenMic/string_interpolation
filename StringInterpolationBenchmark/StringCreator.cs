using BenchmarkDotNet.Attributes;
using System;
using System.Text;

namespace StringInterpolationBenchmark
{
    [MemoryDiagnoser]
    public class StringCreator
    {
        [Params(100, 10_000)]
        public int charNumber { get; set; }

        private readonly Random _random = new Random();

        [Benchmark]
        public string GenerateStringPlusFromRandom()
        {
            string result = string.Empty;

            char offset = 'a';
            int lettersOffset = 26;

            for (var i = 0; i < charNumber; i++)
            {
                var retChar = (char)_random.Next(offset, offset + lettersOffset);
                result += retChar;
            }

            return result;
        }

        [Benchmark]
        public string GenerateStringBuilderFromRandom()
        {
            var result = new StringBuilder();

            char offset = 'a';
            int lettersOffset = 26;

            for (var i = 0; i < charNumber; i++)
            {
                var retChar = (char)_random.Next(offset, offset + lettersOffset);
                result.Append(retChar);
            }

            return result.ToString();
        }

        [Benchmark]
        public string GenerateStringSpanFromRandom()
        {
            unsafe
            {
                Span<char> result = stackalloc char[charNumber];

                char offset = 'a';
                int lettersOffset = 26;

                for (var i = 0; i < charNumber; i++)
                {
                    var retChar = ((char)_random.Next(offset, offset + lettersOffset)).ToString().AsSpan();
                    retChar.CopyTo(result.Slice(i, 1));
                }

                return result.ToString();
            }
        }

        [Benchmark]
        public string GenerateStringSpan()
        {
            unsafe
            {
                Span<char> result = stackalloc char[charNumber];
                ReadOnlySpan<char> chars = "abcdefghijklmnopqrstuvwxyz";

                for (var i = 0; i < charNumber; i++)
                {
                    chars.Slice(_random.Next(26), 1).CopyTo(result.Slice(i, 1));
                }

                return result.ToString();
            }
        }

        [Benchmark]
        public string GenerateStringPlus()
        {
            string result = string.Empty;
            string chars = "abcdefghijklmnopqrstuvwxyz";

            for (var i = 0; i < charNumber; i++)
            {
                result += chars[_random.Next(26)];
            }

            return result;
        }

        [Benchmark]
        public string GenerateStringBuilder()
        {
            var result = new StringBuilder();
            string chars = "abcdefghijklmnopqrstuvwxyz";

            for (var i = 0; i < charNumber; i++)
            {
                result.Append(chars[_random.Next(26)]);
            }

            return result.ToString();
        }
    }
}
