// <copyright file="Program.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerUI;
using WHO.NOK.API.Helper;
using WHO.NOK.Core.ResponseMiddleware;

/// <summary>
/// Program Main Class.
/// </summary>
internal class Program
{
    /// <summary>
    /// Main Method.
    /// </summary>
    /// <param name="args">Arguments.</param>
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigurationManager configuration = builder.Configuration;

        // Add NLog for logging in file.
        builder.Host.ConfigureLogging((hostingContext, logging) =>
        {
            logging.ClearProviders();
            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddNLog();
        });

        builder.Services.ConfigureServices(configuration);

        var app = builder.Build();
        var config = app.Configuration;

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()
        || app.Environment.EnvironmentName.Equals("local"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NOK-API");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
            });
        }

        app.UseCors("CorsPolicy");

        // Add response middleware for the all responses.
        app.UseApiResponseWrapperMiddleware();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        // Localization: Respect Request Localization.
        app.UseRequestLocalization();

        app.MapControllers();

        app.UseStaticFiles();

        app.Run();
    }
}