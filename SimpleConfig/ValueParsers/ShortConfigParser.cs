namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for shorts using short.TryParse./>
/// </summary>
public class ShortConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = short.TryParse(input, out var o);

        output = o;

        return result;
    }
}