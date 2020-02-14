namespace Huten.App.Examples
{
    using System;

    public sealed class Example_05 : Example
    {
        public override void Execute()
        {
            var a = QueryStringBuilder.Create()
                .AppendParameter("array", new object[]
                {
                    1, "2", 3.5
                })
                .Build();

            // "?array=1,2,3.5"
            Console.WriteLine(a);

            var b = QueryStringBuilder.Create()
                .AppendParameter("array", new object[]
                {
                    1, "2",
                    new { A = 5, B = 10 }
                })
                .Build();

            // "?array=1,2&arrayA=5&arrayB=10"
            Console.WriteLine(b);
        }
    }
}