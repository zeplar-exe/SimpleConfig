namespace SimpleConfig.Tests;

[TestFixture]
public class ParserTests
{
    [Test]
    public void TestIntConfiguration()
    {
        AssertThatParsed<int>("A=1", "A", 1);
    }

    [Test]
    public void TestMultiTypeConfiguration()
    {
        var persistentContainer = new ConfigContainer();
        
        AssertThatParsed<int>("A=1", "A", 1, persistentContainer);
        AssertThatParsed<string>("A=\"Hello world!\"", "A", "Hello world!", persistentContainer);
    }
    
    private void AssertThatParsed<T>(string input, string expectedKey, object expectedValue)
    {
        AssertThatParsed<T>(input, expectedKey, expectedValue, new ConfigContainer());
    }

    private void AssertThatParsed<T>(string input, string expectedKey, object expectedValue, ConfigContainer rootContainer)
    {
        var source = new TestValueParserSource();
        var parser = new ConfigParser(source);
        var container = parser.Parse(input);
        rootContainer.AddFrom(container);
        
        Assert.Multiple(() =>
        {
            Assert.That(rootContainer.TryGetOfType<T>(expectedKey, out var value), Is.True);
            Assert.That(value, Is.EqualTo(expectedValue));
        });
    }
}