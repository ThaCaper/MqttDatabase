using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flespi.Core.AppService;
using Flespi.Entity;
using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;

namespace Flespi.REST.API.Mqtt
{
    public class MQTTClientService : IMQTTClientService
    {
        public string Topic = "Topic/Tester";
        private IMqttClient mqttClient;
        private IMqttClientOptions options;
        private readonly ISensorService _sensorService;

        public MQTTClientService(IMqttClientOptions options)
        {
            this.options = options;
            mqttClient = new MqttFactory().CreateMqttClient();
            ConfigureMqttClient();
        }

        public void ConfigureMqttClient()
        {
            mqttClient.ConnectedHandler = this;
            mqttClient.DisconnectedHandler = this;
            mqttClient.ApplicationMessageReceivedHandler = this;
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            Console.WriteLine("### CONNECTED TO SERVER ###");
            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(Topic).Build());

            Console.WriteLine("### SUBSCRIBED ###");
        }
        public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            Console.WriteLine("### DISCONNECTED FROM SERVER ###");
            await Task.Delay(TimeSpan.FromSeconds(5));

            try
            {
                await mqttClient.ConnectAsync(options, CancellationToken.None);
            }
            catch
            {
                Console.WriteLine("### RECONNECTING FAILED ###");
            }

        }
        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            char[] separators = new char[] { ',', ':' };
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            Console.WriteLine($"+ Topic = {eventArgs.ApplicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload).Split(separators, StringSplitOptions.RemoveEmptyEntries).GetValue(3)}");
            Console.WriteLine($"+ QoS = {eventArgs.ApplicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {eventArgs.ApplicationMessage.Retain}");
            Console.WriteLine();

            Sensor sensor = new Sensor
            {
                Id = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload)
                    .Split(separators, StringSplitOptions.RemoveEmptyEntries).GetValue(1).ToString(),
                Temp = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload)
                    .Split(separators, StringSplitOptions.RemoveEmptyEntries).GetValue(3).ToString()
            };

            await Task.FromResult(mqttClient.SubscribeAsync().Result);
           
            _sensorService.CreateSensor(sensor);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await mqttClient.ConnectAsync(options);
            if (!mqttClient.IsConnected)
            {
                await mqttClient.ReconnectAsync();
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        public async Task SubscribeToSensor(string topic)
        {
            var message = await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build());

            Console.WriteLine("### SUBSCRIBED ###");
            Console.WriteLine("### RESULT: " + message.Items.FirstOrDefault()?.ResultCode);
            Console.WriteLine("### RESULT: " + message.Items.FirstOrDefault()?.TopicFilter);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if(cancellationToken.IsCancellationRequested)
            {
                var disconnectOption = new MqttClientDisconnectOptions
                {
                    ReasonCode = MqttClientDisconnectReason.NormalDisconnection,
                    ReasonString = "NormalDiconnection"
                };
                await mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
            }
            await mqttClient.DisconnectAsync();
        }

        
    }
}