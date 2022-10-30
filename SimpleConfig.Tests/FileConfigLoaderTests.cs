namespace SimpleConfig.Tests;

[TestFixture]
public class FileConfigLoaderTests
{
    private static string MockFile => Path.Join(Directory.GetCurrentDirectory(), "Files/my_config.txt");
    
    [TestCase("CAKE", "LIE")]
    [TestCase("WATER", "OIL")]
    [TestCase("WEST", 1)]
    [TestCase("EAST", 2)]
    [TestCase("SOUTH", 3)]
    public void TestFile(string expectedKey, object expectedValue)
    {
        var source = new TestValueParserSource();
        var loader = new FileConfigLoader(source);

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => loader.LoadFile(MockFile));
            Assert.That(loader.TryGet(expectedKey, expectedValue.GetType(), out var value), Is.True);
            Assert.That(value, Is.EqualTo(expectedValue));
        });
    }
}