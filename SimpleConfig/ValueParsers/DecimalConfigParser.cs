namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for decimals using decimal.TryParse./>
/// </summary>
public class DecimalConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = decimal.TryParse(input, out var o);

        output = o;

        return result;
    }
}