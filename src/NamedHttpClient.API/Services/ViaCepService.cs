using NamedHttpClient.API.Constants;
using NamedHttpClient.API.DataTransferObjects.Cep;
using NamedHttpClient.API.Interfaces.Services;
using System.Text.Json;

namespace NamedHttpClient.API.Services;

public sealed class ViaCepService(IHttpClientFactory httpClientFactory) : IViaCepService
{
    public async Task<ViaCepResponse?> GetZipCodeInfoAsync(string zipCode, CancellationToken cancellationToken)
    {
        using var httpClient = httpClientFactory.CreateClient(HttpClientNamesConstants.ViaCepHttpClientName);

        var viaCepHttpResponseMessage = await httpClient.GetAsync($"/ws/{zipCode}/json/", cancellationToken);

        if (!viaCepHttpResponseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        return JsonSerializer.Deserialize<ViaCepResponse>(
            await viaCepHttpResponseMessage.Content.ReadAsStringAsync(cancellationToken),
            _jsonSerializerOptions);
    }

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
