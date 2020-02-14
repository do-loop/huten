namespace Huten.App
{
    using System;
    using System.Collections.Generic;
    using Examples;

    internal sealed class Program
    {
        private static readonly List<Example> Examples = new List<Example>
        {
            new Example_01(),
            new Example_02(),
            new Example_03(),
            new Example_04(),
            new Example_05(),
            new Example_06(),
            new Example_07()
        };

        public static void Main()
        {
            Examples.ForEach(x => x.Execute());

            Console.ReadKey();
        }
    }
}