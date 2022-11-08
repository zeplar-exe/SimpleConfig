using System.Text;

using SimpleConfig.ValueParsers.Sources;

namespace SimpleConfig;

/// <summary>
/// The main parser for configuration key-value pairs.
/// </summary>
public class ConfigParser
{
    private IValueParserSource ValueParserSource { get; }

    /// <summary>
    /// Create a new ConfigParser using an <see cref="AssemblyValueParserSource"/>.
    /// </summary>
    public ConfigParser() : this(new AssemblyValueParserSource())
    {
        
    }

    /// <summary>
    /// Create a new ConfigParser using the provided <see cref="IValueParserSource"/>.
    /// </summary>
    /// <param name="valueParserSource">The parser source to use.</param>
    public ConfigParser(IValueParserSource valueParserSource)
    {
        ValueParserSource = valueParserSource;
    }

    /// <summary>
    /// Parse the specified input. See <see href="https://github.com/zeplar-exe/SimpleConfig">GitHub</see> for
    /// implementation details.
    /// </summary>
    /// <param name="input">The input to parse.</param>
    /// <returns>A <see cref="ConfigContainer"/> created from the parsed input.</returns>
    public ConfigContainer Parse(string input)
    {
        var container = new ConfigContainer();
        var reader = new StringReader(input);

        while (reader.Peek() != -1)
        {
            SkipWhitespace(reader);
            
            var key = new StringBuilder();
            var value = new StringBuilder();

            var foundEquals = false;

            while (reader.Peek() != -1)
            {
                var next = (char)reader.Read();

                if (next == '=')
                {
                    foundEquals = true;
                    
                    break;
                }

                key.Append(next);
            }
            
            if (!foundEquals)
                break;

            while (reader.Peek() != -1)
            {
                var next = (char)reader.Read();

                if (next == ';')
                    break;

                value.Append(next);
            }

            foreach (var parser in ValueParserSource.GetParsers())
            {
                if (parser.TryParse(value.ToString(), out var obj))
                {
                    container.Add(key.ToString(), obj);
                }
            }
        }

        return container;
    }

    private void SkipWhitespace(StringReader reader)
    {
        while (reader.Peek() != -1)
        {
            if (char.IsWhiteSpace((char)reader.Peek()))
                reader.Read();
            else
                return;
        }
    }
}