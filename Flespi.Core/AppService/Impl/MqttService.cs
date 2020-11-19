using System.Threading.Tasks;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace Flespi.Core.AppService.Impl
{
    public class MqttService :  IMqttServerClientConnectedHandler,
                                IMqttServerApplicationMessageInterceptor,
                                IMqttServerConnectionValidator,
                                IMqttServerClientDisconnectedHandler
    {
        public IMqttServer _mqtt;
        public void ConfigureMqttServer(IMqttServer mqtt)
        {
            this._mqtt = mqtt;
            _mqtt.ClientConnectedHandler = this;
            _mqtt.ClientDisconnectedHandler = this;
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            options.WithConnectionValidator(this);
            options.WithApplicationMessageInterceptor(this);
        }

        public Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }

        public Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task ValidateConnectionAsync(MqttConnectionValidatorContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }
    }
}