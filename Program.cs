using System;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AzureGetStarted
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var azureKeyVaultName = "JBReviews-KV3";
                    if (context.HostingEnvironment.IsProduction())
                    {
                        var secretClient = new SecretClient(
                            new Uri($"https://{azureKeyVaultName}.vault.azure.net/"),
                            new DefaultAzureCredential());
                        config.AddAzureKeyVault(secretClient, new HyphenVaultSecretManager());
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class HyphenVaultSecretManager : KeyVaultSecretManager
    {
        public override string GetKey(KeyVaultSecret secret)
        {
            return secret.Name.Replace("-", ConfigurationPath.KeyDelimiter);
        }
    }
}
