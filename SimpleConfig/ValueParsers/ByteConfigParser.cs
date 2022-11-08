namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for bytes using byte.TryParse./>
/// </summary>
public class ByteConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = byte.TryParse(input, out var o);

        output = o;

        return result;
    }
}