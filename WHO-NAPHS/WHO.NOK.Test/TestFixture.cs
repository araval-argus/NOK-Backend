// <copyright file="TestFixture.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WHO.NOK.API.Test.Fixture
{
    using System.Security.Claims;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using WHO.NOK.API.Helper;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Constant;
    using WHO.NOK.Infrastructure.Models.DatabaseContext;

    /// <summary>
    /// TestFixture class.
    /// </summary>
    public class TestFixture
    {
        private static readonly IConfiguration Config;

        private readonly IServiceScopeFactory scopeFactory;
        private readonly ServiceProvider provider;

        static TestFixture()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.local.json")
                .Build();

            CommonSettings.ApplicationSettings = Config.GetSection("AppSettings").Get<ApplicationSettings>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixture"/> class.
        /// </summary>
        public TestFixture()
        {
            var services = new ServiceCollection();

            services.ConfigureServices(Config);
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            // Register the logger factory in the service collection
            services.AddSingleton<ILoggerFactory>(loggerFactory);

            this.provider = services.BuildServiceProvider();

            this.GetDbContext().Database.EnsureCreated();
            this.scopeFactory = this.provider.GetService<IServiceScopeFactory>() !;
        }

        /// <summary>
        /// Gets the DbContext.
        /// </summary>
        /// <returns>Returns instance of <see cref="ApplicationDbContext"/> class.</returns>
        public ApplicationDbContext GetDbContext() => this.provider.GetRequiredService<ApplicationDbContext>();

        /// <summary>
        /// Executes a scope.
        /// </summary>
        /// <param name="action">Delegate to fetch the Required service's instance.</param>
        /// <returns>Instance of the required service.</returns>
        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = this.scopeFactory.CreateScope())
            {
                await action(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Executes a scope.
        /// </summary>
        /// <typeparam name="T">Type T.</typeparam>
        /// <param name="action">Delegate to fetch the Required service's instance.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using (var scope = this.scopeFactory.CreateScope())
            {
                return await action(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Gets the DbContext.
        /// </summary>
        /// <param name="action">Delegate.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task ExecuteDbContextAsync(Func<ApplicationDbContext, Task> action) => this.ExecuteScopeAsync(sp => action(sp.GetService<ApplicationDbContext>() !));

        /// <summary>
        /// Executes the db context.
        /// </summary>
        /// <typeparam name="T">Type T.</typeparam>
        /// <param name="action">Action.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<T> ExecuteDbContextAsync<T>(Func<ApplicationDbContext, Task<T>> action)
        {
            return this.ExecuteScopeAsync(sp => action(sp.GetService<ApplicationDbContext>() !));
        }

        /// <summary>
        /// Gets the required service object.
        /// </summary>
        /// <typeparam name="T">Type T.</typeparam>
        /// <returns>Object of specified service.</returns>
        public async Task<T> GetService<T>()
        {
            return await this.ExecuteScopeAsync((sp) => Task.FromResult(sp.GetService<T>() !));
        }

        /// <summary>
        /// Gets the user claims for testing purpose.
        /// </summary>
        /// <param name="role">Role of the user.</param>
        /// <param name="isReadOnly">Represents the user has rights to only read.</param>
        /// <returns>Instance of ClaimsPrincipal.</returns>
        public ClaimsPrincipal GetUserClaims(Roles role, bool isReadOnly)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(GraphClaimTypes.UserId.ToString(), "1"),
                    new Claim(GraphClaimTypes.Email, "tnaphs@gmail.com"),
                    new Claim(GraphClaimTypes.CountryId, "1"),
                    new Claim(GraphClaimTypes.IsActive, "1"),
                    new Claim(GraphClaimTypes.Role, role.GetHashCode().ToString()),
                    new Claim(GraphClaimTypes.ReadOnly, isReadOnly ? "1" : "0"),
                    new Claim(GraphClaimTypes.LanguageId, "3"),
                }));
            return user;
        }
    }
}
