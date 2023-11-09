using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using App.Grainz.ErrorCodes;

using DotNext;

namespace App.Grainz.Grains
{
    public interface IAppGrain : IGrainWithGuidKey
    {
        public Task<Result<string, ResultErrorCodes>> GetResultString();
        public Task<Result<string, ResultErrorCodes>> GetResultError();
    }
}