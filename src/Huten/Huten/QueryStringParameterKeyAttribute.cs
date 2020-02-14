namespace Huten
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class QueryStringParameterKeyAttribute : Attribute
    {
        public string Key { get; }

        public QueryStringParameterKeyAttribute(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(nameof(key));

            Key = key;
        }
    }
}