namespace SimpleConfig.ValueParsers;

public class ByteConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = byte.TryParse(input, out var o);

        output = o;

        return result;
    }
}