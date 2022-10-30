namespace SimpleConfig.ValueParsers;

public class UIntConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object? output)
    {
        var result = uint.TryParse(input, out var o);

        output = o;

        return result;
    }
}