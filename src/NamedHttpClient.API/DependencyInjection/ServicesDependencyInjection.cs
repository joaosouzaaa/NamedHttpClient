using NamedHttpClient.API.Interfaces.Services;
using NamedHttpClient.API.Services;

namespace NamedHttpClient.API.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IViaCepService, ViaCepService>();
}
