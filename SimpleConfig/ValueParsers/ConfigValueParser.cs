namespace SimpleConfig.ValueParsers;

/// <summary>
/// Base class for parsing value types (not the C# feature).
/// </summary>
public abstract class ConfigValueParser
{
    /// <summary>
    /// Method used by the <see cref="ConfigParser"/>.
    /// </summary>
    /// <param name="input">Full input string to parse.</param>
    /// <param name="output">Parsed value.</param>
    /// <returns>Whether the parse succeeded or failed.</returns>
    public abstract bool TryParse(string input, out object? output);
}