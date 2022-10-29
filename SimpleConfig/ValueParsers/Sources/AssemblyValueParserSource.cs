using System.Reflection;

namespace SimpleConfig.ValueParsers.Sources;

public class AssemblyValueParserSource : IValueParserSource
{
    private static ConfigValueParser[] ValueParsers = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t != typeof(ConfigValueParser) && typeof(ConfigValueParser).IsAssignableFrom(t))
        .Select(Activator.CreateInstance)
        .Cast<ConfigValueParser>()
        .ToArray();


    public ConfigValueParser[] GetParsers()
    {
        return ValueParsers;
    }
}