# SimpleConfig

SimpleConfig is a library featuring a permissive parser for key-value configurations.

# Table of contents

- [Usage](#usage)
  - [In-Code](#in-code)
    - [Sample](#sample)
  - [Syntax](#syntax)
  - [Creating custom value parsers](#creating-custom-value-parsers)
    - [Example](#example)

## Usage

### In-Code

The core functionality is used via the `ConfigParser`. For newcomers, there's the 
`FileConfigLoader` class, which abstracts direct usage of the parser.

#### Sample

<details>
<summary>
See full example code
</summary>

```csharp
using System;
using SimpleConfig;

namespace SimpleConfigSample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var container = new ConfigContainer() { EnforceDistinctTypes = true };
            var parser = new ConfigParser();
        
            var input = Console.ReadLine();
            var parseOutput = parser.Parse(input);
        
            container.AddFrom(parserOutput);
        
            var value = container.GetOfType<string>("SAMPLE_CONFIG");
        
            Console.WriteLine(value);
        }
    }
}
```

<br/>

</details>

```csharp
var container = new ConfigContainer() { EnforceDistinctTypes = true };
```

To start, we create a `ConfigContainer` which simplifies storage and retrieval of configurations.

```cs
var parser = new ConfigParser();
```

Next, we create a `ConfigParser`.

```csharp
var input = Console.ReadLine();
var parseOutput = parser.Parse(input);
```

Then we get some sample input from the user.

```csharp
container.AddFrom(parserOutput);
```

And finally, we add the output from our parser to the original container (the Parse method returns a ConfigContainer).

```csharp
var value = container.GetOfType<string>("SAMPLE_CONFIG");
```

Assuming the input `SAMPLE_CONFIG="ABC"`, `value` will be equal to "ABC".

- If the input does not set `SAMPLE_CONFIG`, this method, this method will throw an exception.
  - An exception will also be thrown if the input does not set `SAMPLE_CONFIG` as a string 
  (surrounded by double quotes)
  - It is recommended to use `container.TryGetOfType(...)` instead for exception safety.
- **Multiple value types can be set to the same key**. `SAMPLE_CONFIG="Hello world!"` and
`SAMPLE_CONFIG=true` are stored as a string and a boolean respectively. This is one of the main reasons
for using `ConfigContainer`.

### Syntax

```
KEY=VALUE;
```

The parser will accept any key, and any value leading up to a final semicolon. 
That is, any character (including whitespace) up to an equals sign is parsed as a key. 
The same goes for a value until a semicolon is reached. Characters will only ever be 
ignored after a semicolon, where all whitespace characters are ignored until a 
non-whitespace character is found.

For implementation details, see the [source code](./SimpleConfig/ConfigParser.cs).

### Creating custom value parsers

Naturally, the built in value parsers will not apply to every codebase's needs. Thus,
users have the ability to create and implement their own by deriving `ConfigValueParser`.

#### Example

```csharp
using System.Numerics;

namespace ConfigValueParserSample;
{
    public class BigIntegerConfigParser : ConfigValueParser
    {
        public override bool TryParse(string input, out object? output)
        {
            var result = BigInteger.TryParse(input, out var o);
    
            output = o; // out output would not work in this case due to the type mismatch
    
            return result;
        }
    }
}
```

Now you have a blueprint, but it still needs to be implemented.

The parser uses config parsers based on what's provided in its `ValueParserSource`.
This is a class implementing `IValueParserSource`, with a `GetParsers` method. By default,
an `AssemblyValueParserSource` is used, which gets every class implementing `ConfigValueParser`
in the current assembly.

```csharp
using System.Reflection;

namespace ConfigValueParserSample;

public class MyValueParserSource : IValueParserSource
{
    private static ConfigValueParser[] ValueParsers =
    {
        new BigIntegerConfigParser()
    }

    public ConfigValueParser[] GetParsers()
    {
        return ValueParsers;
    }
}
```

*Note that GetParsers is called once for every parse call in `ConfigParser`*

Now, this parser source must be passed to config parser.

```csharp
var source = new MyValueParserSource();
var parser = new ConfigParser(source);
```

The same can be done with the builtin `FileConfigLoader`:

```csharp
var source = new MyValueParserSource();
var loader = new FileConfigLoader(source);
```