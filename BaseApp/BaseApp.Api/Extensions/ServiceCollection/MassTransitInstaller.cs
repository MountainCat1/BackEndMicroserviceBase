using MassTransit;

namespace BaseApp.Api.Extensions.ServiceCollection;

public static class MassTransitInstaller
{
    public static IServiceCollection InstallMassTransit(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(2.5);
            });
        services.AddMassTransit(configurator =>
        {
            // configurator.AddConsumer<BusConsumer>();
    
            // Configure Azure Service Bus
            configurator.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("ServiceBus")); 
                // cfg.ReceiveEndpoint("some-queue", endpointConfigurator =>
                // {
                //     endpointConfigurator.ConfigureConsumer<MyBusConsumer>(context);
                // });
                cfg.ConfigureEndpoints(context);
            });
    
        });

        return services;
    }
}