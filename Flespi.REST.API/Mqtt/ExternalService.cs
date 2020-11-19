namespace Flespi.REST.API.Mqtt
{
    public class ExternalService
    {
        private readonly IMQTTClientService _mqttClientService;

        public ExternalService(MqttClientServiceProvider provider)
        {
            _mqttClientService = provider._mqttClientService;
        }
    }
}