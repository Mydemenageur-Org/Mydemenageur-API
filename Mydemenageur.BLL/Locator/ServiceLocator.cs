using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Mydemenageur.DAL.DP.DataProvider;
using Mydemenageur.DAL.DP.Interface;

using Mydemenageur.BLL.Services;
using Mydemenageur.BLL.Services.Interfaces;

namespace Mydemenageur.BLL.Locator
{
    public static class ServiceLocator
    {
        public static void AddBusinessservices(this IServiceCollection services)
        {
            // Business services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<IUsersService, UsersService>();

            // DataProvider services
            services.AddScoped<IDPUser, DPUser>();
        }
    }
}
