namespace SimpleConfig.ValueParsers;

public class ULongConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = ulong.TryParse(input, out var o);

        output = o;

        return result;
    }
}