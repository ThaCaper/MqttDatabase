namespace Flespi.REST.API.Mqtt
{
    public class MqttClientServiceProvider
    {
        public readonly IMQTTClientService _mqttClientService;

        public MqttClientServiceProvider(IMQTTClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
        }
    }
}