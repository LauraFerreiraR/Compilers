using System;
using SimpleInterpreter.Parser;

namespace SimpleInterpreter.Tests
{
    public class LL1ParserTests
    {
        [Fact]
        public void TestLL1ParserSuccess()
        {
            var parser = new LL1Parser("path_to_test_parse_table.csv");
            var tokens = new List<string> { "VAR", "AT", "NUM", "EOF" };
            bool result = parser.Parse(tokens);
            Assert.True(result);
        }

        [Fact]
        public void TestLL1ParserError()
        {
            var parser = new LL1Parser("path_to_test_parse_table.csv");
            var tokens = new List<string> { "VAR", "EOF" };
            bool result = parser.Parse(tokens);
            Assert.False(result);
        }
    }

    internal class FactAttribute : Attribute{
    }
}
