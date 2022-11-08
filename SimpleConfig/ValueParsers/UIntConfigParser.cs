namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for uint using uint.TryParse./>
/// </summary>
public class UIntConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = uint.TryParse(input, out var o);

        output = o;

        return result;
    }
}