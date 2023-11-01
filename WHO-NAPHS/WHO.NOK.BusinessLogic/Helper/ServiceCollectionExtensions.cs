// <copyright file="ServiceCollectionExtensions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Helper
{
    using System.Globalization;
    using System.Reflection;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using WHO.NOK.BusinessLogic.ViewModels.User;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension Method to add all the handler for a particular generic Type T Interface.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <typeparam name="T1">Generic Type to exclude Cache Implementation.</typeparam>
        /// <param name="services">Instance of <see cref="IServiceCollection"/> service.</param>
        /// <param name="assemblies">List of <see cref="Assembly"/> to Scan for the Interface.</param>
        /// <param name="lifetime">Lifespan for the Service.</param>
        public static void RegisterAllTypesWithBaseInterface<T, T1>(this IServiceCollection services, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T)) && !x.GetInterfaces().Contains(typeof(T1))));
            foreach (var type in typesFromAssemblies)
            {
                Type myType = type.GetInterfaces()[0];
                services.Add(new ServiceDescriptor(myType, type, lifetime));
            }
        }

        /// <summary>
        /// Extension Method to add Cache Decorators using scrutor.
        /// </summary>
        /// <typeparam name="T"> Model.</typeparam>
        /// <param name="services"> Service.</param>
        /// <param name="assemblies">List of <see cref="Assembly"/> to Scan for the Interface.</param>
        public static void RegisterCacheDecorators<T>(this IServiceCollection services, Assembly[] assemblies)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                Type myType = type.GetInterfaces()[0];
                services.Decorate(myType, type);
            }

            services.AddMemoryCache();
        }

        /// <summary>
        /// Extension method to register and use .net core localization options.
        /// </summary>
        /// <typeparam name="T"> Model.</typeparam>
        /// <param name="services"> Service.</param>
        public static void RegisterLocalizationOptions(this IServiceCollection services)
        {
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
            opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("fr"),
                };

                opts.DefaultRequestCulture = new RequestCulture("en", "en");

                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;

                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });
        }

        /// <summary>
        /// Register all the validator.
        /// </summary>
        /// <param name="services"> Services.</param>
        public static void RegisterAllValidatorTypesWithBaseInterface(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(UserViewModelValidator));
        }

        /// <summary>
        /// Extension method to Add API Versioning.
        /// </summary>
        /// <param name="services">Services.</param>
        public static void RegisterAPIVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
        }
    }
}