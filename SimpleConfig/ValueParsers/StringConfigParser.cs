namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for strings where any input surrounded by double quotes is valid./>
/// </summary>
public class StringConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        output = default;
        
        if (input.Length > 1 && input.StartsWith("\"") && input.EndsWith("\""))
        {
            output = input.Substring(1, input.Length - 2);
            
            return true;
        }

        return false;
    }
}