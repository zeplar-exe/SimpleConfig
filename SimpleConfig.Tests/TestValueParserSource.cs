using SimpleConfig.ValueParsers;
using SimpleConfig.ValueParsers.Sources;

namespace SimpleConfig.Tests;

public class TestValueParserSource : IValueParserSource
{
    public ConfigValueParser[] GetParsers()
    {
        return new ConfigValueParser[] { new TestIntParser(), new TestStringParser() };
    }
}