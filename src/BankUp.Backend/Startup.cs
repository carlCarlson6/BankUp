using BankUp.Backend.Groups.Infrastructure;
using BankUp.Backend.Infrastructure;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;

namespace BankUp.Backend;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddMongoDb(_configuration)
            .AddMediatR(typeof(Startup))
            .AddGroupsServices()
            .AddFastEndpoints()
            .AddSwaggerDoc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseFastEndpointsMiddleware()
        .UseEndpoints(builder => builder.MapFastEndpoints())
        .UseApimundo()
        .UseOpenApi()
        .UseSwaggerUi3(c => 
            c.ConfigureDefaults());
}