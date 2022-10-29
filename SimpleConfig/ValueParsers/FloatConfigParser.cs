namespace SimpleConfig.ValueParsers;

public class FloatConfigParser : ConfigValueParser
{
    public override bool TryParse(string input, out object output)
    {
        var result = float.TryParse(input, out var o);

        output = o;

        return result;
    }
}