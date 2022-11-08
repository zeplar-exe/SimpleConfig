namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for booleans using bool.TryParse./>
/// </summary>
public class BooleanConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = bool.TryParse(input, out var o);

        output = o;

        return result;
    }
}