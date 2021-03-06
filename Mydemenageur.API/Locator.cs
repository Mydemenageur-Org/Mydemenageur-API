using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Mydemenageur.API.DP.DataProvider;
using Mydemenageur.API.DP.Interface;

using Mydemenageur.API.Services;
using Mydemenageur.API.Services.Interfaces;

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
            services.AddScoped<IServiceProposedService, ServicesProposedService>();

            services.AddScoped<IDPUser, DPUser>();
            services.AddScoped<IDPReview, DPReview>();
            services.AddScoped<IDPHelp, DPHelp>();
            services.AddScoped<IDPService, DPService>();
        }
    }
}
