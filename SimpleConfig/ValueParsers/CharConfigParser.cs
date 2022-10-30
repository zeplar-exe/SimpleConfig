namespace SimpleConfig.ValueParsers;

public class CharConfigParser : ConfigValueParser
{
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