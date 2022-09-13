using Azure.Health.DataServices.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace Health.Integration.Poc
{
    public class Program
    {
        private static ServiceConfig config;
        public static void Main(string[] args)
        {
            IConfigurationBuilder cbuilder = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .AddEnvironmentVariables("AZURE_");
            IConfigurationRoot root = cbuilder.Build();
            config = new();
            root.Bind(config);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //services.UseAuthenticator(options =>
                    //{
                    //    options.ClientId = config.ClientId;
                    //    options.ClientSecret = config.ClientSecret;
                    //    options.TenantId = config.TenantId;
                    //});
                    services.AddSingleton(typeof(ServiceConfig), config);
                    services.AddHostedService<IntegrationService>(); 
                });
        }
    }
}
