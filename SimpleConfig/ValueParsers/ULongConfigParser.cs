namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for ulong using ulong.TryParse./>
/// </summary>
public class ULongConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = ulong.TryParse(input, out var o);

        output = o;

        return result;
    }
}