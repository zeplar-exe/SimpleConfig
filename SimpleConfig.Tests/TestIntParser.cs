using SimpleConfig.ValueParsers;

namespace SimpleConfig.Tests;

public class TestIntParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = int.TryParse(input, out var o);

        output = o;

        return result;
    }
}