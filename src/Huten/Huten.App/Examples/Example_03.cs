namespace Huten.App.Examples
{
    using System;

    public sealed class Example_03 : Example
    {
        public override void Execute()
        {
            var a = QueryStringBuilder.Create()
                .AppendSection("books")
                .AppendParameter("name", "book")
                .Build();

            // "/books?name=book"
            Console.WriteLine(a);

            var b = QueryStringBuilder.Create()
                .AppendSection("books")
                .AppendParameter("name", "book")
                .AppendParameter("price", 70)
                .Build();

            // "/books?name=book&price=70"
            Console.WriteLine(b);
        }
    }
}