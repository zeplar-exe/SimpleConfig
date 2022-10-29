using System.Reflection;
using System.Text;

using SimpleConfig.ValueParsers;

namespace SimpleConfig;

public class ConfigParser
{
    private static ConfigValueParser[] ValueParsers = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t != typeof(ConfigValueParser) && typeof(ConfigValueParser).IsAssignableFrom(t))
        .Select(Activator.CreateInstance)
        .Cast<ConfigValueParser>()
        .ToArray();

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

            foreach (var parser in ValueParsers)
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