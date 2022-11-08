using System.Text;

namespace SimpleConfig;

/// <summary>
/// A wrapper collection to abstract the convoluted storage of parsed config values.
/// </summary>
public class ConfigContainer
{
    private Dictionary<string, List<object?>> Configs { get; }

    /// <summary>
    /// Create a ConfigContainer.
    /// </summary>
    public ConfigContainer()
    {
        Configs = new Dictionary<string, List<object?>>();
    }

    /// <summary>
    /// Add a value under a certain key to the <see cref="ConfigContainer"/>.
    /// </summary>
    /// <param name="key">Config key to sture the value under.</param>
    /// <param name="o">The value to store.</param>
    /// <remarks>
    /// Multiple values can exist under the same key. However, only the first of a specific type will be
    /// retrieved in methods like <see cref="TryGetOfType{T}"/>.
    /// </remarks>
    public void Add(string key, object? o)
    {
        if (!Configs.ContainsKey(key))
        {
            Configs[key] = new List<object?>();
        }

        Configs[key].Add(o);
    }
    
    /// <summary>
    /// Add a range of values under a certain key to the <see cref="ConfigContainer"/>.
    /// </summary>
    /// <param name="key">Config key to sture the values under.</param>
    /// <param name="o">The values to store.</param>
    /// /// <remarks>
    /// Multiple values can exist under the same key. However, only the first of a specific type will be
    /// retrieved in methods like <see cref="TryGetOfType{T}"/>.
    /// </remarks>
    public void AddRange(string key, IEnumerable<object?> o)
    {
        if (!Configs.ContainsKey(key))
        {
            Configs[key] = new List<object?>();
        }

        foreach (var value in o)
        {
            Add(key, value);
        }
    }
    
    /// <param name="key">The key to query.</param>
    /// <returns>Whether the specified key exists in the container.</returns>
    public bool ContainsKey(string key)
    {
        return Configs.ContainsKey(key);
    }

    /// <summary>
    /// Adds configurations from another <see cref="ConfigContainer"/>.
    /// </summary>
    /// <param name="container">The target container to pull configurations from.</param>
    /// /// /// <remarks>
    /// Multiple values can exist under the same key. However, only the first of a specific type will be
    /// retrieved in methods like <see cref="TryGetOfType{T}"/>.
    /// </remarks>
    public void AddFrom(ConfigContainer container)
    {
        foreach (var pair in container.Configs)
        {
            AddRange(pair.Key, pair.Value);
        }
    }
    
    /// <returns>Every configuration <b>value</b> stored in this container.</returns>
    public IEnumerable<object?> GetValues()
    {
        return Configs.SelectMany(c => c.Value);
    }

    /// <returns>Every registered key in the container.</returns>
    public IEnumerable<string> GetKeys()
    {
        return Configs.Keys;
    }

    /// <param name="key">The key to get values from.</param>
    /// <returns>Every configuration <b>value</b> stored in this container under the specified key.</returns>
    public IEnumerable<object?> GetValuesOf(string key)
    {
        return Configs[key];
    }
    
    /// <param name="key">The key to search.</param>
    /// <typeparam name="T">The type to retrieve.</typeparam>
    /// <returns>The retrieved value of the specified type.</returns>
    /// /// <exception cref="KeyNotFoundException">The specified key was not found in the container.</exception>
    public T? GetOfType<T>(string key)
    {
        return (T?)GetOfType(key, typeof(T));
    }
    
    /// <param name="key">The key to search for.</param>
    /// <param name="type">The type to retrieve.</param>
    /// <returns>The retrieved value of the specified type.</returns>
    /// <exception cref="KeyNotFoundException">The specified key was not found in the container.</exception>
    public object? GetOfType(string key, Type type)
    {
        try
        {
            return Configs[key].First(c => c?.GetType() == type);
        }
        catch (InvalidOperationException)
        {
            throw new KeyNotFoundException($"The key '{key}' does not exist in the container.");
        }
        
    }

    /// <param name="key">The key to search.</param>
    /// <param name="value">The retrieved value.</param>
    /// <typeparam name="T">The type to retrieve.</typeparam>
    /// <returns>Whether the key of the specified type was found.</returns>
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

    /// <param name="key">The key to search.</param>
    /// <param name="type">The type to retrieve.</param>
    /// <param name="value">The retrieved value.</param>
    /// <returns>Whether the key of the specified type was found.</returns>
    public bool TryGetOfType(string key, Type type, out object? value)
    {
        value = default;
        object? valueHolder = default;

        if (!Configs.ContainsKey(key))
            return false;
        
        var found = Configs[key].Any(v =>
        {
            if (v?.GetType() == type)
            {
                valueHolder = v;

                return true;
            }
                    
            return false;
        });

        value = valueHolder;

        return found;
    }

    /// <summary>
    /// Clears the container's contents.
    /// </summary>
    public void Clear()
    {
        Configs.Clear();
    }

    /// <summary>
    /// Converts the container to a string.
    /// </summary>
    /// <returns>The stringified version of this container.</returns>
    /// <remarks>
    /// - The ToString method is used for every instance <br/>
    /// - null is converted to "null" (without the quotations) <br/>
    /// - Duplicate keys exist one after the other for multi-type key-value pairs
    /// </remarks>
    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var (key, values) in Configs)
        {
            foreach (var value in values)
            {
                builder.Append(key).Append('=');
                
                if (value == null)
                {
                    builder.AppendLine(value != null ? value.ToString() : "null");
                }
            }
        }
        
        return builder.ToString();
    }
}