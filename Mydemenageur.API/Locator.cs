using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Mydemenageur.DAL.DP.DataProvider;
using Mydemenageur.DAL.DP.Interface;

using Mydemenageur.BLL.Services;
using Mydemenageur.BLL.Services.Interfaces;

namespace Mydemenageur.API
{
    public static class Locator
    {
        public static void InitLocator(this IServiceCollection services, IConfiguration configuration)
        {
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

            services.AddScoped<IDPUser, DPUser>();
            services.AddScoped<IDPReview, DPReview>();
            services.AddScoped<IDPHelp, DPHelp>();
        }
    }
}
