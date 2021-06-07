using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mydemenageur.API;
using Mydemenageur.API.Services;
using Mydemenageur.API.Services.Interfaces;
using Mydemenageur.API.Settings;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mydemenageur.ScenarioTests.Helpers
{
    public class FakeStartup
    { 
        public FakeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoSettings>(Configuration.GetSection(nameof(MongoSettings)));
            services.Configure<MydemenageurSettings>(Configuration.GetSection(nameof(MydemenageurSettings)));

            services.AddSingleton<IMongoSettings>(Span => Span.GetRequiredService<IOptions<MongoSettings>>().Value);
            services.AddSingleton<IMydemenageurSettings>(Span => Span.GetRequiredService<IOptions<MydemenageurSettings>>().Value);

            // JWT Authentication
            var mydemenageurSettings = Configuration.GetSection(nameof(MydemenageurSettings)).Get<MydemenageurSettings>();
            var key = Encoding.ASCII.GetBytes(mydemenageurSettings.ApiSecret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddControllers();

            services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("Mydemenageur.API"))); //"IntegrationTestMVC" is your original project name

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<IHousingsService, HousingsService>();
            services.AddScoped<IMoveRequestsService, MoveRequestsService>();
            services.AddScoped<IMoversService, MoversService>();
            services.AddScoped<ISocietiesService, SocietiesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IVehiclesService, VehiclesService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mydemenageur.API", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Mydemenageur.API.xml");
                c.IncludeXmlComments(filePath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mydemenageur.API v1");
                c.InjectStylesheet("/swagger/themes/theme-flattop.css");
                c.InjectJavascript("/swagger/custom-script.js", "text/javascript");
                c.RoutePrefix = "documentation";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
