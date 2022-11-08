namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for ushort using ushort.TryParse./>
/// </summary>
public class UShortConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = ushort.TryParse(input, out var o);

        output = o;

        return result;
    }
}