namespace Huten.App.Examples
{
    using System;

    public sealed class Example_02 : Example
    {
        public override void Execute()
        {
            var a = QueryStringBuilder.Create()
                .AppendSection("document")
                .AppendSection(666.ToString())
                .Build();

            // "/document/666"
            Console.WriteLine(a);

            var b = QueryStringBuilder.Create()
                .AppendSection("users")
                .AppendSection(666.ToString())
                .AppendSection("name")
                .Build();

            // "/users/666/name"
            Console.WriteLine(b);

            var c = QueryStringBuilder.Create("https://mysite.com")
                .AppendSection("api")
                .AppendSection("users")
                .AppendSection(666.ToString())
                .AppendSection("name")
                .Build();

            // "https://mysite.com/api/users/666/name"
            Console.WriteLine(c);
        }
    }
}