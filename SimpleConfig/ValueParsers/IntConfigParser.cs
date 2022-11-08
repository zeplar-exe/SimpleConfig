namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for integers using int.TryParse./>
/// </summary>
public class IntConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        var result = int.TryParse(input, out var o);

        output = o;

        return result;
    }
}