namespace Huten
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Extensions;
    using Formatters.Base;

    public sealed class QueryStringBuilder
    {
        private string _baseUrl;

        private UriKind _uriKind;

        private readonly List<string> _sections = new List<string>();

        private readonly Dictionary<string, string> _parameters = new Dictionary<string, string>();

        public static QueryStringBuilder Create() => new QueryStringBuilder();

        public static QueryStringBuilder Create(string baseUrl) => new QueryStringBuilder(baseUrl);

        public QueryStringBuilder() => Clear();

        public QueryStringBuilder(string baseUrl) => Clear(baseUrl);

        public QueryStringBuilder Clear()
        {
            _baseUrl = string.Empty;
            _uriKind = UriKind.RelativeOrAbsolute;

            _sections.Clear();
            _parameters.Clear();

            return this;
        }

        public QueryStringBuilder Clear(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException(nameof(baseUrl));

            _uriKind = UriKind.Absolute;

            if (TryCreate(baseUrl, out _) == false)
                throw new ArgumentException(nameof(baseUrl));

            _baseUrl = baseUrl;

            _sections.Clear();
            _parameters.Clear();

            return this;
        }

        public QueryStringBuilder AppendSection(string section)
        {
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException(nameof(section));

            _sections.Add(section.Trim('/', ' '));

            return this;
        }

        public QueryStringBuilder AppendParameter(string key, string value, bool @override = true)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(nameof(key));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            _parameters[key] = _parameters.ContainsKey(key)
                ? @override ? value.Trim() : string.Join(",", _parameters[key], value)
                : value;

            return this;
        }

        public QueryStringBuilder AppendParameter(string key, string value, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(culture), @override);

        public QueryStringBuilder AppendParameter(string key, Enum value, bool @override = true)
            => AppendParameter(key, Convert.ToInt32(value), @override);

        public QueryStringBuilder AppendParameter(string key, int value, bool @override = true)
            => AppendParameter(key, value.ToString(), @override);

        public QueryStringBuilder AppendParameter(string key, int value, string format, bool @override = true)
            => AppendParameter(key, value.ToString(format), @override);

        public QueryStringBuilder AppendParameter(string key, int value, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(culture), @override);

        public QueryStringBuilder AppendParameter(string key, int value, string format, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(format, culture), @override);

        public QueryStringBuilder AppendParameter(string key, long value, bool @override = true)
            => AppendParameter(key, value.ToString(), @override);

        public QueryStringBuilder AppendParameter(string key, long value, string format, bool @override = true)
            => AppendParameter(key, value.ToString(format), @override);

        public QueryStringBuilder AppendParameter(string key, long value, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(culture), @override);

        public QueryStringBuilder AppendParameter(string key, long value, string format, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(format, culture), @override);

        public QueryStringBuilder AppendParameter(string key, short value, bool @override = true)
            => AppendParameter(key, value.ToString(), @override);

        public QueryStringBuilder AppendParameter(string key, short value, string format, bool @override = true)
            => AppendParameter(key, value.ToString(format), @override);

        public QueryStringBuilder AppendParameter(string key, short value, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(culture), @override);

        public QueryStringBuilder AppendParameter(string key, short value, string format, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(format, culture), @override);

        public QueryStringBuilder AppendParameter(string key, DateTime value, bool @override = true)
            => AppendParameter(key, value.ToString(CultureInfo.InvariantCulture), @override);

        public QueryStringBuilder AppendParameter(string key, DateTime value, string format, bool @override = true)
            => AppendParameter(key, value.ToString(format), @override);

        public QueryStringBuilder AppendParameter(string key, bool value, bool @override = true)
            => AppendParameter(key, value.ToString(), @override);

        public QueryStringBuilder AppendParameter(string key, double value, bool @override = true)
            => AppendParameter(key, value.ToString(CultureInfo.InvariantCulture), @override);

        public QueryStringBuilder AppendParameter(string key, double value, string format, bool @override = true)
            => AppendParameter(key, value.ToString(format), @override);

        public QueryStringBuilder AppendParameter(string key, double value, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(culture), @override);

        public QueryStringBuilder AppendParameter(string key, double value, string format, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(format, culture), @override);

        public QueryStringBuilder AppendParameter(string key, decimal value, bool @override = true)
            => AppendParameter(key, value.ToString(CultureInfo.InvariantCulture), @override);

        public QueryStringBuilder AppendParameter(string key, decimal value, string format, bool @override = true)
            => AppendParameter(key, value.ToString(format), @override);

        public QueryStringBuilder AppendParameter(string key, decimal value, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(culture), @override);

        public QueryStringBuilder AppendParameter(string key, decimal value, string format, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(format, culture), @override);

        public QueryStringBuilder AppendParameter(string key, float value, bool @override = true)
            => AppendParameter(key, value.ToString(CultureInfo.InvariantCulture), @override);

        public QueryStringBuilder AppendParameter(string key, float value, string format, bool @override = true)
            => AppendParameter(key, value.ToString(format), @override);

        public QueryStringBuilder AppendParameter(string key, float value, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(culture), @override);

        public QueryStringBuilder AppendParameter(string key, float value, string format, CultureInfo culture, bool @override = true)
            => AppendParameter(key, value.ToString(format, culture), @override);

        public QueryStringBuilder AppendParameter(string key, IEnumerable values, bool @override = true)
        {
            _parameters.Remove(key);

            foreach (var value in values)
            {
                if (value == null)
                    continue;

                Process(key, value, @override: false);
            }

            return this;
        }

        public QueryStringBuilder AppendParameter(string key, IEnumerable values, string format, bool @override = true)
        {
            _parameters.Remove(key);

            foreach (var value in values)
            {
                if (value == null)
                    continue;

                var method = AppendMethod(typeof(string), GetType(value), typeof(string), typeof(bool));

                var _ = method == null
                    ? Process(key, value, @override: false)
                    : method.Invoke(this, new[] { key, value, format, false });
            }

            return this;
        }

        public QueryStringBuilder AppendParameters(IReadOnlyDictionary<string, object> parameters, bool @override = true)
        {
            if (parameters.IsEmpty())
                return this;

            foreach (var (key, value) in parameters)
            {
                if (value == null)
                    continue;

                Process(key, value, @override);
            }

            return this;
        }

        public QueryStringBuilder ExtractParameters(object @object, bool @override = true)
        {
            if (@object == null)
                throw new ArgumentNullException(nameof(@object));

            return ExtractParameters(string.Empty, @object, @override);
        }

        private QueryStringBuilder ExtractParameters(string prefix, object @object, bool @override = true)
        {
            if (prefix == null)
                throw new ArgumentNullException(nameof(prefix));

            if (@object == null)
                throw new ArgumentNullException(nameof(@object));

            var properties = @object.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead)
                .ToArray();

            foreach (var property in properties)
            {
                var value = property.GetValue(@object, null);
                if (value == default)
                    continue;

                var key = ExpandKey(prefix, property);
                var format = property.GetCustomAttribute<QueryStringParameterFormatAttribute>();

                if (format == null)
                {
                    Process(key, value, @override);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(format.Format) == false)
                {
                    var method = AppendMethod(typeof(string), GetType(value), typeof(string), typeof(bool));

                    var _ = method == null
                        ? Process(key, value, @override)
                        : method.Invoke(this, new[] { key, value, format.Format, @override });

                    continue;
                }

                if (format.Formatter != null)
                {
                    var method = AppendMethod(typeof(string), GetType(value), typeof(QueryStringParameterFormatter), typeof(bool));

                    var _ = method == null
                        ? Process(key, value, @override)
                        : method.Invoke(this, new[] { key, value, format.Formatter, @override });
                }
            }

            return this;
        }

        public Uri Build()
        {
            var builder = new StringBuilder(_baseUrl);

            if (_sections.IsNotEmpty())
            {
                if (!_baseUrl.EndsWith("/"))
                    builder.Append("/");

                builder.Append(string.Join("/", _sections));
            }

            if (_parameters.IsNotEmpty())
            {
                var parameters = _parameters.Select(x => string.Join("=", x.Key, x.Value));

                builder
                    .Append("?")
                    .Append(string.Join("&", parameters));
            }

            var @string = builder.ToString();
            if (string.IsNullOrWhiteSpace(@string))
                return null;

            if (TryCreate(@string, out var uri))
                return uri;

            throw new InvalidOperationException("Не удалось создать Url.");
        }

        public QueryStringBuilder AppendParameter(string key, IEnumerable values, QueryStringParameterFormatter formatter, bool @override)
        {
            _parameters.Remove(key);

            foreach (var value in values)
            {
                if (value == null)
                    continue;

                var method = AppendMethod(typeof(string), GetType(value), typeof(QueryStringParameterFormatter), typeof(bool));

                var _ = method == null
                    ? Process(key, value, false)
                    : method.Invoke(this, new[] { key, value, formatter, false });
            }

            return this;
        }

        private QueryStringBuilder Process(string key, object value, bool @override)
        {
            var method = AppendMethod(typeof(string), GetType(value), typeof(bool));

            return method == null
                ? ExtractParameters(key, value, @override)
                : (QueryStringBuilder) method.Invoke(this, new[] { key, value, @override });
        }

        private static Type GetType(object value)
        {
            var type = value.GetType();

            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
                return typeof(IEnumerable);

            return type.IsEnum ? typeof(Enum) : type;
        }

        private MethodInfo AppendMethod(params Type[] types)
        {
            var type = GetType();

            const BindingFlags binding = BindingFlags.Public | 
                                         BindingFlags.NonPublic | 
                                         BindingFlags.Instance;

            return type.GetMethod(nameof(AppendParameter), binding, null, types, null);
        }

        private static string ExpandKey(string key, MemberInfo property)
        {
            return key + (property.GetCustomAttribute<QueryStringParameterKeyAttribute>()?.Key ?? property.Name);
        }

        private bool TryCreate(string value, out Uri uri) => Uri.TryCreate(value, _uriKind, out uri);

        private QueryStringBuilder AppendParameter(string key, string value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, DateTime value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, int value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, long value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, short value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, Enum value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, double value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, bool value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, decimal value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);

        private QueryStringBuilder AppendParameter(string key, float value, QueryStringParameterFormatter formatter, bool @override)
            => AppendParameter(key, formatter.Format(value), @override);
    }
}