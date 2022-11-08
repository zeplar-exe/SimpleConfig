namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for doubles using double.TryParse./>
/// </summary>
public class DoubleConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = double.TryParse(input, out var o);

        output = o;

        return result;
    }
}