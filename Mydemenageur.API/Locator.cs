using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Mydemenageur.BLL.Locator;

namespace Mydemenageur.API
{
    public static class Locator
    {
        public static void InitLocator(this IServiceCollection services, IConfiguration configuration)
        {
            // service locator for business and data provider services
            services.AddBusinessservices();

            // for others services like SMTP services etc...
        }
    }
}
