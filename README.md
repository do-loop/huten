# huten
Simple query string builder implementation.

#### [Examples](https://github.com/do-loop/huten/tree/master/src/Huten/Huten.App/Examples)

#### Simple Examples

```csharp

// "https://mysite.com/api/users/666/name"
QueryStringBuilder.Create("https://mysite.com")
    .AppendSection("api")
    .AppendSection("users")
    .AppendSection(666.ToString())
    .AppendSection("name")
    .Build();

// "/books?name=book&price=70"
QueryStringBuilder.Create()
    .AppendSection("books")
    .AppendParameter("name", "book")
    .AppendParameter("price", 70)
    .Build();

var parameters = new Dictionary<string, object>
{
    ["name"] = "book", ["price"] = 70
};

// "/books?name=book&price=70"
QueryStringBuilder.Create()
    .AppendSection("books")
    .AppendParameters(parameters)
    .Build();

var elements = new object[] { 1, "2", 3.5 };

// "?array=1,2,3.5"
QueryStringBuilder.Create()
    .AppendParameter("array", elements)
    .Build();

```

#### Object Examples (1)

```csharp

QueryStringBuilder.Create()
    .ExtractParameters(new Request())
    .Build();

public enum MyEnum { A = 10, B = 20 }

// "?Age=5&Name=Victor"
public sealed class Request
{
    public int Age { get; set; } = 5;
    
    public string Name { get; set; } = "Victor";
}

// "?True=1&False=False
public sealed class Request
{
    [QueryStringParameterFormat(typeof(IntegerBooleanParameterFormatter))]
    public bool True { get; set; } = true;
    
    public bool False { get; set; } = false;
}

// "?True=1&F=False&A=10&B=B
public sealed class Request
{
    [QueryStringParameterFormat(typeof(IntegerBooleanParameterFormatter))]
    public bool True { get; set; } = true;

    [QueryStringParameterKey("F")]
    public bool False { get; set; } = false;

    public MyEnum A { get; set; } = MyEnum.A;

    [QueryStringParameterFormat(typeof(StringEnumParameterFormatter))]
    public MyEnum B { get; set; } = MyEnum.B;
}

// "?A=10,20&B=A,B
public sealed class Request
{
    public MyEnum[] A { get; set; } = { MyEnum.A, MyEnum.B };

    [QueryStringParameterFormat(typeof(StringEnumParameterFormatter))]
    public MyEnum[] B { get; set; } = { MyEnum.B, MyEnum.A };
}

```

#### Object Examples (2)

```csharp

QueryStringBuilder.Create()
    .ExtractParameters(new Request())
    .Build();

// "?Age=5&MyName=Victor&SomeValue=100&SomeName=Victor"
public sealed class Request
{
    public int Age { get; set; } = 5;

    [QueryStringParameterKey("MyName")]
    public string Name { get; set; } = "Victor";

    [QueryStringParameterKey("Some")]
    public Inner Inner { get; set; } = new Inner();
}

public sealed class Inner
{
    public int Value { get; set; } = 100;
    
    public string Name { get; set; } = "Victor";
}

```
