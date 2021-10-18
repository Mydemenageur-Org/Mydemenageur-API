using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mydemenageur.DAL.Settings;
using Mydemenageur.DAL.Settings.Interfaces;
using System.IO;
using System.Text;

namespace Mydemenageur.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_developerPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoSettings>(Configuration.GetSection(nameof(MongoSettings)));
            services.Configure<MydemenageurSettings>(Configuration.GetSection(nameof(MydemenageurSettings)));
            services.InitLocator(Configuration);
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
            .AddJwtBearer("Default", x =>
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
            })
            .AddJwtBearer("Firebase", options =>
            {
                options.Authority = "https://securetoken.google.com/mydemenageur-v2";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "mydemenageur-v2",
                    ValidateAudience = true,
                    ValidAudience = "mydemenageur-v2",
                    ValidateLifetime = true
                };
            });


            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000/", "http://my-demenageur.local:3000", "https://my-demenageur.local:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mydemenageur.API", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Mydemenageur.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mydemenageur.API v1");
                c.InjectStylesheet("/swagger/themes/theme-flattop.css");
                c.InjectJavascript("/swagger/custom-script.js", "text/javascript");
                c.RoutePrefix = "documentation";
            });


            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

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
