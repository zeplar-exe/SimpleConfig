namespace SimpleConfig.ValueParsers;

public class DoubleConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = double.TryParse(input, out var o);

        output = o;

        return result;
    }
}