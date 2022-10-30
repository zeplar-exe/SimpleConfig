namespace SimpleConfig.ValueParsers;

public class LongConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = long.TryParse(input, out var o);

        output = o;

        return result;
    }
}