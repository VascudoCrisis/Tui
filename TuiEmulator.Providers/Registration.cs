using Microsoft.Extensions.DependencyInjection;
using TuiEmulator.Common;
using TuiEmulator.Providers.Providers;
using TuiEmulator.Providers.Repositories;
using TuiEmulator.Providers.Repositories.Abstractions;

namespace TuiEmulator.Providers
{
    public static class Registration
    {
        public static IServiceCollection RegisterDal(this IServiceCollection services)
        {
            return services
                .AddSingleton<ILocationsRepository, LocationsRepository>()
                .RegisterProviders()
                .AddSingleton<ISearchService, ProviderAggregator>()
                .AddSingleton<IDictService, ProviderAggregator>();
        }

        private static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            return services
                .AddSingleton<IToursProviderService, TuiProvider>()
                .AddSingleton<IToursProviderService, OtherProvider>();
        }
    }
}