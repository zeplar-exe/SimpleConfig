using SimpleConfig.ValueParsers.Sources;

namespace SimpleConfig;

/// <summary>
/// A sample/abstract usage of the <see cref="ConfigParser"/> to load files into a <see cref="ConfigContainer"/>.
/// </summary>
public class FileConfigLoader
{
    private IValueParserSource ValueParserSource { get; }
    /// <summary>
    /// The <see cref="ConfigContainer"/> of this config loader.
    /// </summary>
    public ConfigContainer Container { get; }

    /// <summary>
    /// Create a <see cref="FileConfigLoader"/> with an empty <see cref="ConfigContainer"/>
    /// and a <see cref="AssemblyValueParserSource"/>.
    /// </summary>
    public FileConfigLoader() : this(new ConfigContainer(), new AssemblyValueParserSource())
    {
        
    }

    /// <summary>
    /// Create a <see cref="FileConfigLoader"/> with the specified <see cref="IValueParserSource"/> and
    /// an empty <see cref="ConfigContainer"/>.
    /// </summary>
    public FileConfigLoader(IValueParserSource valueParserSource) : this(new ConfigContainer(), valueParserSource)
    {
        
    }

    /// <summary>
    /// Create a <see cref="FileConfigLoader"/> with the specified <see cref="ConfigContainer"/>
    /// and <see cref="IValueParserSource"/>.
    /// </summary>
    public FileConfigLoader(ConfigContainer container, IValueParserSource valueParserSource)
    {
        Container = container;
        ValueParserSource = valueParserSource;
    }

    /// <summary>
    /// Get a value of the specified type at the specified key.
    /// </summary>
    /// <param name="key">The key to search.</param>
    /// <typeparam name="T">The type to search for in the key.</typeparam>
    /// <returns>The value at the specified key of the specified type.</returns>
    public T? Get<T>(string key)
    {
        return Container.GetOfType<T>(key);
    }
    
    /// <summary>
    /// Get a value of the specified type at the specified key.
    /// </summary>
    /// <param name="key">The key to search.</param>
    /// <param name="type">The type to search for in the key.</param>
    /// <returns>The value at the specified key of the specified type.</returns>
    public object? Get(string key, Type type)
    {
        return Container.GetOfType(key, type);
    }

    /// <summary>
    /// Attempt to get a value of the specified type at the specified key.
    /// </summary>
    /// <param name="key">The key to search.</param>
    /// <param name="value">The retrieved value.</param>
    /// <typeparam name="T">The type to search for in the key.</typeparam>
    /// <returns>Whether the value was successfully retrieved.</returns>
    public bool TryGet<T>(string key, out T? value)
    {
        return Container.TryGetOfType(key, out value);
    }

    /// <summary>
    /// Attempt to get a value of the specified type at the specified key.
    /// </summary>
    /// <param name="key">The key to search.</param>
    /// <param name="type">The type to search for in the key.</param>
    /// <param name="value">The retrieved value.</param>
    /// <returns>Whether the value was successfully retrieved.</returns>
    public bool TryGet(string key, Type type, out object? value)
    {
        return Container.TryGetOfType(key, type, out value);
    }

    /// <summary>
    /// Loads the file into this loader's <see cref="Container"/>.
    /// </summary>
    /// <param name="file">The file path to read from.</param>
    /// <remarks>
    /// Regular IO exceptions may apply.
    /// </remarks>
    public void LoadFile(string file)
    {
        var text = File.ReadAllText(file);
        var parser = new ConfigParser(ValueParserSource);
        parser.Parse(text);

        Container.AddFrom(parser.Parse(text));
    }
    
    /// <summary>
    /// Attempt to load the given file path.
    /// </summary>
    /// <param name="file">The file path to read from.</param>
    /// <returns>Whether the file was successfully found, read from, and added to the <see cref="Container"/>.</returns>
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

    /// <summary>
    /// Clear the underlying <see cref="ConfigContainer"/>.
    /// </summary>
    public void Clear()
    {
        Container.Clear();
    }
}