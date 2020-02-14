namespace Huten.App.Examples
{
    using System;
    using Formatters;

    public sealed class Example_06 : Example
    {
        public sealed class Request_1
        {
            public int Age { get; set; } = 5;

            public string Name { get; set; } = "Victor";
        }

        public sealed class Request_2
        {
            [QueryStringParameterFormat(typeof(IntegerBooleanParameterFormatter))]
            public bool True { get; set; } = true;

            public bool False { get; set; } = false;
        }

        public enum MyEnum { A = 10, B = 20 }

        public sealed class Request_3
        {
            [QueryStringParameterFormat(typeof(IntegerBooleanParameterFormatter))]
            public bool True { get; set; } = true;

            [QueryStringParameterKey("F")]
            public bool False { get; set; } = false;

            public MyEnum A { get; set; } = MyEnum.A;

            [QueryStringParameterFormat(typeof(StringEnumParameterFormatter))]
            public MyEnum B { get; set; } = MyEnum.B;
        }

        public sealed class Request_4
        {
            public MyEnum[] A { get; set; } = { MyEnum.A, MyEnum.B };

            [QueryStringParameterFormat(typeof(StringEnumParameterFormatter))]
            public MyEnum[] B { get; set; } = { MyEnum.B, MyEnum.A };
        }

        public override void Execute()
        {
            var a = QueryStringBuilder.Create()
                .ExtractParameters(new Request_1())
                .Build();

            // "?Age=5&Name=Victor"
            Console.WriteLine(a);

            var b = QueryStringBuilder.Create()
                .ExtractParameters(new Request_2())
                .Build();

            // "?True=1&False=False
            Console.WriteLine(b);

            var c = QueryStringBuilder.Create()
                .ExtractParameters(new Request_3())
                .Build();

            // "?True=1&F=False&A=10&B=B
            Console.WriteLine(c);

            var d = QueryStringBuilder.Create()
                .ExtractParameters(new Request_4())
                .Build();

            // "?A=10,20&B=B,A
            Console.WriteLine(d);
        }
    }
}