using Microsoft.AspNetCore.Mvc;
using NamedHttpClient.API.DataTransferObjects.Cep;
using NamedHttpClient.API.Interfaces.Services;

namespace NamedHttpClient.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ViaCepController(IViaCepService viaCepService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViaCepResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<ViaCepResponse?> GetZipCodeInfoAsync([FromQuery] string zipCode, CancellationToken cancellationToken) =>
        viaCepService.GetZipCodeInfoAsync(zipCode, cancellationToken);
}
