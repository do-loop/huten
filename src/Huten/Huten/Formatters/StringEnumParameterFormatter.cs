namespace Huten.Formatters
{
    using System;
    using Base;

    public sealed class StringEnumParameterFormatter : QueryStringEnumParameterFormatter
    {
        public override string Format(Enum value)
        {
            return value.ToString();
        }
    }
}