namespace Huten.Formatters.Base
{
    using System;

    public abstract class QueryStringEnumParameterFormatter : QueryStringParameterFormatter<Enum>
    {
        public abstract override string Format(Enum value);
    }
}