namespace Huten.App.Examples
{
    using System;
    using Formatters;

    public sealed class Example_07 : Example
    {
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

        public override void Execute()
        {
            var a = QueryStringBuilder.Create()
                .ExtractParameters(new Request())
                .Build();

            // "?Age=5&MyName=Victor&SomeValue=100&SomeName=Victor"
            Console.WriteLine(a);
        }
    }
}