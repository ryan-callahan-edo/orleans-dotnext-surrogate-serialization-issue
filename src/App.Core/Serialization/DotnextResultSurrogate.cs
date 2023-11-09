using DotNext;
using DotNext.Runtime;

namespace App.Grainz.Serialization
{
    [GenerateSerializer]
    public struct DotnextResultSurrogate<T, TError>
    where TError : struct, Enum
    {
        [Id(0)]
        public T value;
        [Id(1)]
        public TError errorCode;
        public DotnextResultSurrogate(T value)
        {
            this.value = value;
            errorCode = default;
        }
        public DotnextResultSurrogate(TError error)
        {
            value = default!;
            errorCode = Intrinsics.IsDefault(in error) ? throw new ArgumentOutOfRangeException(nameof(error)) : error;
        }

    }

    [RegisterConverter]
    public sealed class DotnextResultSurrogateConverter<T, TError> :
        IConverter<Result<T, TError>, DotnextResultSurrogate<T, TError>>
        where TError : struct, Enum
    {
        public Result<T, TError> ConvertFromSurrogate(
            in DotnextResultSurrogate<T, TError> surrogate) =>
            Intrinsics.IsDefault(in surrogate.errorCode) ? new(surrogate.errorCode) : new(surrogate.value);

        public DotnextResultSurrogate<T, TError> ConvertToSurrogate(
            in Result<T, TError> value) =>
            value.IsSuccessful ? new(value.Error) : new(value.Value);
    }
}
