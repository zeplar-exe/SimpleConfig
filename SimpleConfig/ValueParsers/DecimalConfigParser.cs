namespace SimpleConfig.ValueParsers;

public class DecimalConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = decimal.TryParse(input, out var o);

        output = o;

        return result;
    }
}