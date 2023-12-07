using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BaseApp.Api.Installers;

public static class AzureSecretsInstaller
{
    public static ConfigurationManager InstallAzureSecrets(this ConfigurationManager configuration)
    {
        string kvUri = configuration["KeyVault:Uri"]!;
        string kvClientId = configuration["KeyVault:ClientId"]!;
        string kvTenantId = configuration["KeyVault:TenantId"]!;
        string kvClientSecret = configuration.GetValue<string>("AZURE_KEY_VAULT_SECRET")!;
        
        var credential = new ClientSecretCredential(kvTenantId, kvClientId, kvClientSecret);
        var client = new SecretClient(new Uri(kvUri), credential);
        configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
        
        return configuration;
    }
}