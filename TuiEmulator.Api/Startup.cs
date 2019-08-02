using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TuiEmulator.Common.Options;
using TuiEmulator.Providers;

namespace TuiEmulator.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiOptions>(_configuration.GetSection("ApiOptions"));
            services.Configure<ProviderOptions>("Tui", _configuration.GetSection("ProviderOptions:Tui"));
            services.Configure<ProviderOptions>("Other", _configuration.GetSection("ProviderOptions:Other"));
            services.Configure<ProviderAggregatorOptions>(_configuration.GetSection("ProviderAggregatorOptions"));

            services.RegisterDal()
                .AddMvcCore()
                .AddJsonFormatters()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}