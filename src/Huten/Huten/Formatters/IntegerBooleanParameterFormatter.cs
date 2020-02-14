namespace Huten.Formatters
{
    using Base;

    public sealed class IntegerBooleanParameterFormatter : QueryStringParameterFormatter<bool>
    {
        public override string Format(bool value)
        {
            return (value ? 1 : 0).ToString();
        }
    }
}