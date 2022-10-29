namespace SimpleConfig;

public class ConfigContainer
{
    private Dictionary<string, List<object?>> Configs { get; }

    public ConfigContainer()
    {
        Configs = new Dictionary<string, List<object?>>();
    }

    public void Add(string key, object o)
    {
        if (!Configs.ContainsKey(key))
        {
            Configs[key] = new List<object?>();
        }

        Configs[key].Add(o);
    }
    
    public void AddRange(string key, IEnumerable<object> o)
    {
        if (!Configs.ContainsKey(key))
        {
            Configs[key] = new List<object>();
        }

        foreach (var value in o)
        {
            Add(key, value);
        }
    }

    public bool ContainsKey(string key)
    {
        return Configs.ContainsKey(key);
    }

    public void AddFrom(ConfigContainer container)
    {
        foreach (var pair in container.Configs)
        {
            AddRange(pair.Key, pair.Value);
        }
    }
    
    public IEnumerable<object?> GetAll(string key)
    {
        return Configs[key];
    }

    public T? GetOfType<T>(string key)
    {
        return (T?)Configs[key].First(c => c is T?);
    }
    
    public bool TryGetOfType<T>(string key, out T? value)
    {
        value = default;
        
        if (TryGetOfType(key, typeof(T), out var tempValue))
        {
            value = (T?)tempValue;

            return true;
        }

        return false;
    }

    public bool TryGetOfType(string key, Type type, out object? value)
    {
        value = default;
        object? valueHolder = default;
        
        var found = Configs.Any(p =>
        {
            if (p.Value.Any(v => v.GetType() == type))
            {
                valueHolder = p.Value;

                return true;
            }
                    
            return false;
        });

        value = valueHolder;

        return found;
    }
}