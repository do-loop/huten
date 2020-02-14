namespace Huten.Formatters.Base
{
    using System;

    public abstract class QueryStringParameterFormatter
    {
        public abstract string Format(object value);
    }

    public abstract class QueryStringParameterFormatter<T> : QueryStringParameterFormatter
    {
        public override string Format(object value)
        {
            var type = value.GetType();

            if (typeof(T) == typeof(Enum) && type.IsEnum || type == typeof(T))
                return Format((T) value);

            throw new ArgumentException("Неверный тип переданного значения.");
        }

        public abstract string Format(T value);
    }
}