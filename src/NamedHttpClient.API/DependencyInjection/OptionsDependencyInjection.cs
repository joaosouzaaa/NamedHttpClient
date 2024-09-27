using NamedHttpClient.API.Constants;
using NamedHttpClient.API.Options;

namespace NamedHttpClient.API.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<HttpClientOptions>(configuration.GetSection(OptionsConstants.HttpClientSection));
}
