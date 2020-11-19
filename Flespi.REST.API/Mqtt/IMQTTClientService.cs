using Microsoft.Extensions.Hosting;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Receiving;

namespace Flespi.REST.API.Mqtt
{
    public interface IMQTTClientService :   IHostedService,
                                            IMqttClientConnectedHandler,
                                            IMqttClientDisconnectedHandler, 
                                            IMqttApplicationMessageReceivedHandler
    {
        
    }
}