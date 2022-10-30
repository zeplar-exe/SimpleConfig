namespace SimpleConfig.ValueParsers;

public class ShortConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = short.TryParse(input, out var o);

        output = o;

        return result;
    }
}