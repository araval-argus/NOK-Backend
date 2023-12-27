// <copyright file="ServiceConfigurationExtensions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8601

namespace WHO.NOK.API.Helper
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;
    using WHO.NOK.API.Helper.Configuration;
    using WHO.NOK.BusinessLogic.CacheInterfaces;
    using WHO.NOK.BusinessLogic.Helper;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.Core.Constant;
    using WHO.NOK.Infrastructure;
    using WHO.NOK.Infrastructure.Models.DatabaseContext;
    using WHO.NOK.Infrastructure.ServiceImplementation;

    /// <summary>
    /// Contains for extension methods of IServiceCollection interface.
    /// </summary>
    public static class ServiceConfigurationExtensions
    {
        /// <summary>
        /// Configures services.
        /// </summary>
        /// <param name="services">Instance of IServiceCollection.</param>
        /// <param name="configuration">Instance of IConfiguration to provide configuration.</param>
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Register IHttpContextAccessor for services to get access to the HttpContext.
            services.AddHttpContextAccessor();

            // Add the database context
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    b => b.MigrationsAssembly("WHO.NOK.API")));

                    // Get AppSettings from database.
            IConfiguration customConfiguration = new ConfigurationBuilder()
            .Add(new AppSettingsConfigurationSource(GetDbContext(services)))
            .Build();

            string jsonAppSettings = customConfiguration["AppSettings"];
            CommonSettings.ApplicationSettings = ApplicationSettings.FromJson(jsonAppSettings);

            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer(options =>
            //         {
            //             options.RequireHttpsMetadata = false;
            //             string tenant = CommonSettings.ApplicationSettings!.TenantId;
            //             string stsDiscoveryEndpoint = $"https://login.microsoftonline.com/{tenant}/v2.0/.well-known/openid-configuration";

            //             // Note: Uncomment following line to show complete trace for debugging. It should be commented in prod.
            //             // Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            //             var configManager = new Microsoft.IdentityModel.Protocols.ConfigurationManager<OpenIdConnectConfiguration>(
            //                 stsDiscoveryEndpoint,
            //                 new OpenIdConnectConfigurationRetriever());
            //             OpenIdConnectConfiguration config = configManager.GetConfigurationAsync().GetAwaiter().GetResult();
            //             options.TokenValidationParameters = new TokenValidationParameters
            //             {
            //                 ValidAudience = string.Empty,
            //                 ValidateAudience = false,
            //                 ValidateIssuer = false,
            //                 ValidateLifetime = true,
            //                 ValidateIssuerSigningKey = false,
            //                 IssuerSigningKeys = config.SigningKeys,
            //             };
            //         });

            // Add the scope service for user claims
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IClaimsTransformation, UserClaimsTransformation>();

            // Add automapper support.
            services.AddAutoMapper(typeof(AutomappingProfile));

            List<string> allowedCorsOriginsEndPoints = $"{configuration["CSRFDomains"]}".Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
            services.AddCors(options =>
                {
                    options.AddPolicy(
                        "CorsPolicy",
                        builder => builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(allowedCorsOriginsEndPoints.ToArray())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });

            // Setup the form options.
            services.Configure<FormOptions>(o =>
                        {
                            o.ValueLengthLimit = int.MaxValue;
                            o.MultipartBodyLengthLimit = int.MaxValue;
                            o.MemoryBufferThreshold = int.MaxValue;
                        });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NOK-API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                Array.Empty<string>()
            },
                });
            });

            // Register All Services As Scoped Service
            services.RegisterAllTypesWithBaseInterface<IBaseService, ICacheService>(new[] { typeof(UserService).Assembly }, ServiceLifetime.Scoped);

            // Register Cache Services as Decorators to Service Implementations
            // services.RegisterCacheDecorators<ICacheService>(new[] { typeof(CachedUserService).Assembly });

            // Register all the validator.
            services.RegisterAllValidatorTypesWithBaseInterface();

            // Localization
            services.RegisterLocalizationOptions();

            // Add authorization for active user only.
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllowActiveUserOnly", policy =>
                            policy.RequireAssertion(context =>
                            GraphClaimsPrincipalExtensions.GetUser(context.User) !.Enabled
                                                                .GetValueOrDefault()));
            });
        }

        /// <summary>
        /// Gets the ApplicationDbContext.
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/> class.</param>
        /// <returns>Returns instance of <see cref="AppDbContext"/> class.</returns>
        private static ApplicationDbContext GetDbContext(IServiceCollection services)
        {
            return services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
        }
    }
}