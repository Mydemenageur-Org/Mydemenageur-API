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
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<IHousingsService, HousingsService>();
            services.AddScoped<IMoveRequestsService, MoveRequestsService>();
            services.AddScoped<IMoversService, MoversService>();
            services.AddScoped<ISocietiesService, SocietiesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IVehiclesService, VehiclesService>();
            services.AddScoped<IPastActionsService, PastActionsService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IHelpService, HelpService>();

            // DataProvider services
            services.AddScoped<IDPUser, DPUser>();
            services.AddScoped<IDPReview, DPReview>();
            services.AddScoped<IDPHelp, DPHelp>();
        }
    }
}
