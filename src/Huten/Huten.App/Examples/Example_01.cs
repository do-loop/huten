namespace Huten.App.Examples
{
    using System;

    public sealed class Example_01 : Example
    {
        public override void Execute()
        {
            var a = new QueryStringBuilder();

            // "null"
            Console.WriteLine(a.Build()?.ToString() ?? "null");

            var b = QueryStringBuilder.Create();

            // "null"
            Console.WriteLine(b.Build()?.ToString() ?? "null");

            var c = QueryStringBuilder.Create("https://google.com");

            // "https://google.com/"
            Console.WriteLine(c.Build());
        }
    }
}