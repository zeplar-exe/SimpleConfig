namespace SimpleConfig.ValueParsers;

public class IntConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object output)
    {
        var result = int.TryParse(input, out var o);

        output = o;

        return result;
    }
}