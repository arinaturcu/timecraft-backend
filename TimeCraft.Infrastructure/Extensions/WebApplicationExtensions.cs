﻿using Microsoft.AspNetCore.Builder;
using Serilog;
using TimeCraft.Infrastructure.Middlewares;

namespace TimeCraft.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// This extension method adds all the configuration for the application that is about to run.
    /// </summary>
    public static WebApplication ConfigureApplication(this WebApplication application)
    {
        application.UseMiddleware<GlobalExceptionHandlerMiddleware>() // Adds the global exception handler middleware.
            .UseSwagger() // Adds the swagger.
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeCraft app v1")) // Add the swagger UI with the application name.
            .UseCors() // Sets to use the CORS configuration.
            .UseRouting() // Adds routing.
            .UseAuthentication() // Adds authentication.
            .UseSerilogRequestLogging() // Adds advanced logging using the Serilog NuGets.
            .UseAuthorization(); // Adds authorization to verify the JWT.
        application.MapControllers(); // Adds controller mappings.

        return application;
    }
}
