using SimpleConfig.ValueParsers;

namespace SimpleConfig.Tests;

public class TestStringParser : ConfigValueParser
{
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