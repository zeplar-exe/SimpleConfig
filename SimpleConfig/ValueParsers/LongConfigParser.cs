namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for longs using long.TryParse./>
/// </summary>
public class LongConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = long.TryParse(input, out var o);

        output = o;

        return result;
    }
}