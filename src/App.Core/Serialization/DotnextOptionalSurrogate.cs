using System.Diagnostics.CodeAnalysis;
using DotNext;

namespace App.Grainz.Serialization
{
    [GenerateSerializer]
    public struct DotnextOptionalSurrogate<T>
    {
        public const byte UndefinedValue = 0;
        public const byte NullValue = 1;
        public const byte NotEmptyValue = 3;
        public static readonly bool IsOptional;

        static DotnextOptionalSurrogate()
        {
            var type = typeof(T);
            IsOptional = type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Optional<>);
        }

        [Id(0)]
        public readonly T? value;
        [Id(1)]
        public readonly byte kind;


        public DotnextOptionalSurrogate(T? value)
        {
            this.value = value;
            kind = value is null ? NullValue : IsOptional ? GetKindUnsafe(ref value) : NotEmptyValue;
        }

        public static byte GetKindUnsafe([DisallowNull] ref T optionalValue)
        {
            return optionalValue.Equals(null)
                ? NullValue
                : optionalValue.Equals(new object())
                ? UndefinedValue
                : NotEmptyValue;
        }

    }

    [RegisterConverter]
    public sealed class DotnextOptionalSurrogateConverter<T> :
        IConverter<Optional<T>, DotnextOptionalSurrogate<T>>
    {
        public Optional<T> ConvertFromSurrogate(in DotnextOptionalSurrogate<T> surrogate) => new(surrogate.value);

        public DotnextOptionalSurrogate<T> ConvertToSurrogate(in Optional<T> value) =>
            value.HasValue ? default : new(value.Value);
    }
}
