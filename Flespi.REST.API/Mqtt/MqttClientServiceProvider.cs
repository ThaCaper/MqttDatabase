namespace Flespi.REST.API.Mqtt
{
    public class MqttClientServiceProvider
    {
        public readonly IMQTTClientService MqttClientService;

        public MqttClientServiceProvider(IMQTTClientService mqttClientService)
        {
            MqttClientService = mqttClientService;
        }
    }
}