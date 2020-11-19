using System;
using Flespi.Infrastructure.SQL;
using Flespi.REST.API.Mqtt;
using Flespi.REST.API.Options;
using Flespi.REST.API.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.Client.Options;

namespace Flespi.REST.API.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMqttClientHostedService(this IServiceCollection services)
        {
            services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
            {
                var clientSettinigs = AppSettingsProvider.ClientSettings;
                var brokerHostSettings = AppSettingsProvider.BrokerHostSettings;

                aspOptionBuilder
                    .WithCredentials(clientSettinigs.UserName, clientSettinigs.Password)
                    .WithClientId(clientSettinigs.Id)
                    .WithTcpServer(brokerHostSettings.Host, brokerHostSettings.Port);
            });
            return services;
        }

        private static IServiceCollection AddMqttClientServiceWithConfig(this IServiceCollection services, Action<MqttClientOptionBuilder> configure)
        {
            services.AddSingleton<IMqttClientOptions>(serviceProvider =>
            {
                var optionBuilder = new MqttClientOptionBuilder(serviceProvider);
                configure(optionBuilder);
                return optionBuilder.Build();
            });
            services.AddSingleton<MQTTClientService>();
            services.AddSingleton<IHostedService>(serviceProvider => serviceProvider.GetService<MQTTClientService>());
            services.AddSingleton<MqttClientServiceProvider>(serviceProvider =>
            {
                var mqttClientService = serviceProvider.GetService<MQTTClientService>();
                var mqttClientServiceProvider = new MqttClientServiceProvider(mqttClientService);
                return mqttClientServiceProvider;
            });
            return services;
        }
    }
}