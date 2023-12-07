using Newtonsoft.Json;

namespace BaseApp.Api.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static TConfiguration GetConfiguration<TConfiguration>(
            this ConfigurationManager configurationManager,
            string sectionName)
            where TConfiguration : new()
        {
            var configuration = new TConfiguration();

            var section = configurationManager.GetSection(sectionName);

            section.Bind(configuration);

            return configuration;
        }

        public static TConfiguration GetConfiguration<TConfiguration>(this ConfigurationManager configurationManager)
            where TConfiguration : new()
        {
            return configurationManager.GetConfiguration<TConfiguration>(typeof(TConfiguration).Name);
        }

        public static TConfiguration GetSecret<TConfiguration>(this ConfigurationManager configuration)
            where TConfiguration : new()
        {
            var config = new TConfiguration();

            var sectionName = typeof(TConfiguration).Name;

            var section = configuration.GetSection(sectionName);

            // If value is null means that this section has sub section which means it was loaded as a json file
            // so we just treat is as configuration
            if (section.Value is null)
            {
                return configuration.GetConfiguration<TConfiguration>();
            }
        
            // otherwise assume that we loading from azure key vault which means we need to deserialize the value
            var secretValue = configuration[sectionName];

            if (string.IsNullOrEmpty(secretValue))
            {
                return config;
            }

            config = JsonConvert.DeserializeObject<TConfiguration>(secretValue); 
            return config;
        }

        public static T GetRequiredEnvironmentVariable<T>(this ConfigurationManager configuration, string variableName)
        {
            var value = configuration.GetValue<T>(variableName);
        
            if(value is null)
                throw new InvalidOperationException($"Required environment variable {variableName} is not set");
        
            return value;
        }
    
        public static string GetRequiredConnectionString(this ConfigurationManager configuration, string connectionStringName)
        {
            var connectionString = configuration.GetConnectionString(connectionStringName);
        
            if(string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException($"Required connection string {connectionStringName} is not set");
        
            return connectionString;
        }
    }
}