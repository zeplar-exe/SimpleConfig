namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for floats using float.TryParse./>
/// </summary>
public class FloatConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = float.TryParse(input, out var o);

        output = o;

        return result;
    }
}