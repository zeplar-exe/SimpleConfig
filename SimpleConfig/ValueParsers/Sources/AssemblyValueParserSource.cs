using System.Reflection;

namespace SimpleConfig.ValueParsers.Sources;

/// <summary>
/// <see cref="IValueParserSource"/> implementation which supplies an instance of every <see cref="ConfigValueParser"/>
/// in the current assembly.
/// </summary>
/// <remarks>
/// This implementation uses <see cref="Assembly.GetExecutingAssembly"/>.
/// </remarks>
public class AssemblyValueParserSource : IValueParserSource
{
    private static ConfigValueParser[] ValueParsers = Assembly
        .GetExecutingAssembly().GetTypes()
        .Where(t => t != typeof(ConfigValueParser) && typeof(ConfigValueParser).IsAssignableFrom(t))
        .Select(Activator.CreateInstance)
        .Cast<ConfigValueParser>()
        .ToArray();


    /// <inheritdoc />
    public ConfigValueParser[] GetParsers()
    {
        return ValueParsers;
    }
}