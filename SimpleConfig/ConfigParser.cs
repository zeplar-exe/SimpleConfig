using System.Text;

using SimpleConfig.ValueParsers.Sources;

namespace SimpleConfig;

public class ConfigParser
{
    private IValueParserSource ValueParserSource { get; }

    public ConfigParser(IValueParserSource valueParserSource)
    {
        ValueParserSource = valueParserSource;
    }

    public ConfigContainer Parse(string input)
    {
        var container = new ConfigContainer();
        var reader = new StringReader(input);

        while (reader.Peek() != -1)
        {
            SkipWhitespace(reader);
            
            var key = new StringBuilder();
            var value = new StringBuilder();

            while (reader.Peek() != -1)
            {
                var next = (char)reader.Read();

                if (next == '=')
                    break;

                key.Append(next);
            }

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