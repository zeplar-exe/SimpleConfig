using SimpleConfig.ValueParsers.Sources;

namespace SimpleConfig;

public class FileConfigLoader
{
    private IValueParserSource ValueParserSource { get; }
    public ConfigContainer Container { get; }

    public FileConfigLoader() : this(new ConfigContainer(), new AssemblyValueParserSource())
    {
        
    }

    public FileConfigLoader(IValueParserSource valueParserSource) : this(new ConfigContainer(), valueParserSource)
    {
        
    }

    public FileConfigLoader(ConfigContainer container, IValueParserSource valueParserSource)
    {
        Container = container;
        ValueParserSource = valueParserSource;
    }

    public T? Get<T>(string key)
    {
        return Container.GetOfType<T>(key);
    }
    
    public bool TryGet<T>(string key, out T? value)
    {
        return Container.TryGetOfType(key, out value);
    }
    
    public bool TryLoadFile(string file)
    {
        try
        {
            var text = File.ReadAllText(file);
            var parser = new ConfigParser(ValueParserSource);

            Container.AddFrom(parser.Parse(text));
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}