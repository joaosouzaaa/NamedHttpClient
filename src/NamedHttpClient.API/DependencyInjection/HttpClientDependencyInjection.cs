using NamedHttpClient.API.Constants;
using NamedHttpClient.API.Options;

namespace NamedHttpClient.API.DependencyInjection;

internal static class HttpClientDependencyInjection
{
    internal static void AddHttpClientDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        var httpClient = configuration.GetSection(OptionsConstants.HttpClientSection).Get<HttpClientOptions>()!;

        services.AddHttpClient(HttpClientNamesConstants.ViaCepHttpClientName, h => h.BaseAddress = new Uri(httpClient.ViaCepBaseAddress));
    }
}
