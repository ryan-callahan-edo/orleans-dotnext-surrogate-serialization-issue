using App.Grainz.Grains;

using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ResultsController(ILogger<ResultsController> logger, IGrainFactory grainFactory) : ControllerBase
{
    private readonly IGrainFactory _grainFactory = grainFactory;

    private readonly ILogger<ResultsController> _logger = logger;


    [HttpGet(Name = "GetResults")]
    public async void Get()
    {
        IAppGrain grain = _grainFactory.GetGrain<IAppGrain>(new Guid());
        var result = await grain.GetResultString();
        _logger.LogInformation(result.IsSuccessful.ToString());
        _logger.LogInformation(result.Value);

        result = await grain.GetResultError();
        _logger.LogInformation(result.IsSuccessful.ToString());
        _logger.LogInformation(result.Error.ToString());
    }
}
