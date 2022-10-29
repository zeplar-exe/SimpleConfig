namespace SimpleConfig.ValueParsers;

public abstract class ConfigValueParser
{
    public abstract bool TryParse(string input, out object? output);
}