namespace SimpleConfig;

public class ConfigContainer
{
    private Dictionary<string, List<object>> Configs { get; }
    
    public bool EnforceDistinctTypes { get; set; }

    public ConfigContainer()
    {
        Configs = new Dictionary<string, List<object>>();
    }

    public void Add(string key, object o)
    {
        if (!Configs.ContainsKey(key))
        {
            Configs[key] = new List<object>();
        }

        if (EnforceDistinctTypes)
        {
            string matchingKey = ""; 
            
            if (Configs.Any(p =>
                {
                    if (p.Value.GetType() == o.GetType())
                    {
                        matchingKey = p.Key;

                        return true;
                    }
                    
                    return false;
                }))
            {
                Configs.Remove(matchingKey);
                // matchingKey is guaranteed to be an actual key by this point
            }
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

    public T GetOfType<T>(string key)
    {
        return (T)Configs[key].First(c => c is T);
    }
}