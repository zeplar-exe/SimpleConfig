namespace SimpleConfig;

public class FileConfigLoader : IConfigLoader
{
    private ConfigContainer Container { get; }

    public FileConfigLoader()
    {
        Container = new ConfigContainer();
    }

    public FileConfigLoader(ConfigContainer container)
    {
        Container = container;
    }

    public T Get<T>(string key)
    {
        return Container.GetOfType<T>(key);
    }
    
    public bool TryLoadFile(string file)
    {
        try
        {
            var text = File.ReadAllText(file);
            var parser = new ConfigParser();

            Container.AddFrom(parser.Parse(text));
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}