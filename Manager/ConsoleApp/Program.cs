using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SQS;
using ConsoleApp.Interface;
using ConsoleApp.Services;
using Manager;
using Manager.Interface;
using Manager.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConfigurationBuilder builder = new();
            BuildConfig(builder);

            Console.WriteLine("Application Starting");

            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    IConfiguration configuration = context.Configuration;

                    services.Configure<AWSSQSServiceConfiguration>(configuration.GetSection("Amazon"));

                    string ak = configuration.GetSection("Amazon:AWSSQS:AccessKeyId").Value;
                    string sk = configuration.GetSection("Amazon:AWSSQS:SecretAccessKey").Value;
                    string rn = configuration.GetSection("Amazon:AWSSQS:Region").Value;

                    services.AddAWSService<IAmazonSQS>(new AWSOptions()
                    {
                        Credentials = new BasicAWSCredentials(ak, sk),
                        Region = Amazon.RegionEndpoint.GetBySystemName(rn)
                    });

                    services.AddTransient<IAWSSQSHelper, AWSSQSHelper>();
                    services.AddTransient<IAWSSQSAppService, AWSSQSAppService>();
                })
                .UseSerilog()
                .Build();

            AWSSQSAppService svc = ActivatorUtilities.CreateInstance<AWSSQSAppService>(host.Services);
            await svc.RunAsync();
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.jon.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
