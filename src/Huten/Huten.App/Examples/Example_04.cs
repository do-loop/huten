namespace Huten.App.Examples
{
    using System;
    using System.Collections.Generic;

    public sealed class Example_04 : Example
    {
        public override void Execute()
        {
            var a = QueryStringBuilder.Create()
                .AppendSection("books")
                .AppendParameters(new Dictionary<string, object>
                {
                    ["name"] = "book",
                    ["price"] = 70
                })
                .Build();

            // "/books?name=book&price=70"
            Console.WriteLine(a);

            var b = QueryStringBuilder.Create()
                .AppendSection("books")
                .AppendParameters(new Dictionary<string, object>
                {
                    ["object"] = new
                    {
                        A = 5,
                        B = "string"
                    }
                })
                .Build();

            // "/books?objectA=5&objectB=string"
            Console.WriteLine(b);
        }
    }
}