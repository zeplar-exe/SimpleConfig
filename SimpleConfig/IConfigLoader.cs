namespace SimpleConfig;

public interface IConfigLoader
{
    public T Get<T>(string key);
}