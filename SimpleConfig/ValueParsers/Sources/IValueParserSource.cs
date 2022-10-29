namespace SimpleConfig.ValueParsers.Sources;

public interface IValueParserSource
{
    public ConfigValueParser[] GetParsers();
}