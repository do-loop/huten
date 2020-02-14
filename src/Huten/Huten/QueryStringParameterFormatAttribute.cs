namespace Huten
{
    using System;
    using Formatters.Base;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class QueryStringParameterFormatAttribute : Attribute
    {
        public string Format { get; }

        public QueryStringParameterFormatter Formatter { get; }

        public QueryStringParameterFormatAttribute(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentException(nameof(format));

            Format = format;
        }

        public QueryStringParameterFormatAttribute(Type formatter, params object[] arguments)
        {
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            if (typeof(QueryStringParameterFormatter).IsAssignableFrom(formatter) == false)
                throw new ArgumentException(nameof(formatter));

            Formatter = (QueryStringParameterFormatter) Activator.CreateInstance(formatter, arguments);
        }
    }
}