namespace SimpleConfig.ValueParsers;

public class UShortConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = ushort.TryParse(input, out var o);

        output = o;

        return result;
    }
}