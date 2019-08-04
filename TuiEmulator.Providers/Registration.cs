using Microsoft.Extensions.DependencyInjection;
using TuiEmulator.Common.Services;
using TuiEmulator.Providers.Providers;
using TuiEmulator.Providers.Repositories;
using TuiEmulator.Providers.Repositories.Abstractions;

namespace TuiEmulator.Providers
{
    public static class Registration
    {
        /// <summary>
        ///     Регистрация DAL
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection RegisterDal(this IServiceCollection services)
        {
            return services
                .AddSingleton<ILocationsRepository, LocationsRepository>()
                .RegisterProviders()
                .AddSingleton<ISearchService, ProviderAggregator>()
                .AddSingleton<IDictService, ProviderAggregator>();
        }

        /// <summary>
        ///     Регистрация поставщиков туров
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        private static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            return services
                .AddSingleton<IToursProviderService, TuiProvider>()
                .AddSingleton<IToursProviderService, OtherProvider>();
        }
    }
}