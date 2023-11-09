using App.Grainz.ErrorCodes;

using DotNext;

namespace App.Grainz.Grains
{
    public class AppGrain : Grain, IAppGrain
    {
        public Task<Result<string, ResultErrorCodes>> GetResultString()
        {
            return Task.FromResult(new Result<string, ResultErrorCodes>("No Error"));
        }

        public Task<Result<string, ResultErrorCodes>> GetResultError()
        {
            return Task.FromResult(new Result<string, ResultErrorCodes>(ResultErrorCodes.GenericError));
        }
    }
}