using Microsoft.Extensions.Options;
using TuiEmulator.Common.Options;
using TuiEmulator.Providers.Providers.Abstractions;
using TuiEmulator.Providers.Repositories.Abstractions;

namespace TuiEmulator.Providers.Providers
{
    internal class OtherProvider : ProviderBase
    {
        public OtherProvider(ILocationsRepository locationsRepository, IOptionsMonitor<ProviderOptions> options)
            : base(locationsRepository, options.Get("Other"))
        {
        }
    }
}