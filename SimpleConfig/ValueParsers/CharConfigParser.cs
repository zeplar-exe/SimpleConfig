namespace SimpleConfig.ValueParsers;

/// <summary>
/// Config parser for singular characters (that is, inputs of 1 length).
/// </summary>
public class CharConfigParser : ConfigValueParser
{
    /// <inheritdoc />
    public override bool TryParse(string input, out object? output)
    {
        output = default;
        
        if (input.Length == 1)
        {
            output = input[0];
            
            return true;
        }

        return false;
    }
}