using System.IO;
using System.Text;
using System.Threading.Tasks;
using BankUp.Backend.Infrastructure;
using DotNet.Testcontainers.Containers.Builders;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace BankUp.Backend.Test.Api;

public class BaseApiTest : IAsyncLifetime
{
    private readonly MongoDbTestContainerConfiguration _mongoDbConfig = 
        new(MongoDbTestContainerConfiguration.MongoDbImage, MongoDbTestContainerConfiguration.MongoDbPort)
        {
            Database = "BankUpDb",
            Username = "BankUpDb_admin",
            Password = "BankUpDb_pw"
        };

    private readonly MongoDbTestContainer _mongoContainer;

    protected BaseApiTest() =>
        _mongoContainer = new TestcontainersBuilder<MongoDbTestContainer>()
            .WithDatabase(_mongoDbConfig)
            .Build();

    protected IWebHost GivenTestHost() =>
        WebHost
            .CreateDefaultBuilder()
            .UseStartup<Startup>()
            .UseEnvironment(HostEnvironmentExtensions.ApiTestsEnvironmentName)
            .UseTestServer()
            .ConfigureAppConfiguration((_, builder) => builder
                .AddMongoDbConfiguration(new MongoDbConfiguration { DatabaseName = _mongoDbConfig.Database, ConnectionString = _mongoContainer.ConnectionString })
                .AddEnvironmentVariables())
            .ConfigureTestServices(services => services.AddFakeAuthenticationHandler())
            .UseDefaultServiceProvider((_, options) =>
            {
                // makes sure DI lifetimes and scopes don't have common issues
                options.ValidateScopes = true;
                options.ValidateOnBuild = true;
            })
            .Start();

    public Task InitializeAsync() => _mongoContainer.StartAsync();
    
    public Task DisposeAsync() => _mongoContainer.DisposeAsync().AsTask();
}

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddMongoDbConfiguration(this IConfigurationBuilder builder, MongoDbConfiguration mongoDbConfiguration)
    {
        var byteArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(mongoDbConfiguration));
        var stream = new MemoryStream(byteArray);
        return builder.AddJsonStream(stream);
    }
}