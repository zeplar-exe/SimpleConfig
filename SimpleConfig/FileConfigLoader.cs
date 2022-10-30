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
    
    public object? Get(string key, Type type)
    {
        return Container.GetOfType(key, type);
    }
    
    public bool TryGet<T>(string key, out T? value)
    {
        return Container.TryGetOfType(key, out value);
    }
    
    public bool TryGet(string key, Type type, out object? value)
    {
        return Container.TryGetOfType(key, type, out value);
    }

    public void LoadFile(string file)
    {
        var text = File.ReadAllText(file);
        var parser = new ConfigParser(ValueParserSource);
        parser.Parse(text);

        Container.AddFrom(parser.Parse(text));
    }
    
    public bool TryLoadFile(string file)
    {
        try
        {
            LoadFile(file);
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Clear()
    {
        Container.Clear();
    }
}