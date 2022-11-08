namespace SimpleConfig.ValueParsers.Sources;

/// <summary>
/// Interface to supply <see cref="ConfigValueParser"/> instances to a <see cref="ConfigParser"/>.
/// </summary>
public interface IValueParserSource
{
    /// <returns><see cref="ConfigValueParser"/> <b>instances</b> to be used in the parsing process.</returns>
    public ConfigValueParser[] GetParsers();
}