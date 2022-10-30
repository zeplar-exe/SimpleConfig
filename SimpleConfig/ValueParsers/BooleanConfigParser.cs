namespace SimpleConfig.ValueParsers;

public class BooleanConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = bool.TryParse(input, out var o);

        output = o;

        return result;
    }
}