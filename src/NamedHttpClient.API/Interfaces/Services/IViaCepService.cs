using NamedHttpClient.API.DataTransferObjects.Cep;

namespace NamedHttpClient.API.Interfaces.Services;

public interface IViaCepService
{
    Task<ViaCepResponse?> GetZipCodeInfoAsync(string zipCode, CancellationToken cancellationToken);
}
