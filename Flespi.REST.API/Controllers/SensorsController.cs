using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flespi.Core.AppService;
using Flespi.Entity;
using Flespi.REST.API.Mqtt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace Flespi.REST.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;
        private readonly IMQTTClientService _mqttClientService;
        public SensorsController(ISensorService sensorService, MqttClientServiceProvider provider)
        {
            _sensorService = sensorService;
            _mqttClientService = provider.MqttClientService;
        }
        // GET: <SensorsController>
        [HttpGet]
        public ActionResult<IEnumerable<Sensor>> GetAllSensors()
        {
            return _sensorService.GetAllSensors();
        }

        // GET <SensorsController>/5
        [HttpGet("{id}")]
        public ActionResult<Sensor> GetSensor(string id)
        {
            return _sensorService.GetSensorById(id);
        }

        // POST <SensorsController>
        [HttpPost]
        public async Task<ActionResult<Sensor>> Post([FromQuery] Sensor sensor, MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            await _mqttClientService.HandleApplicationMessageReceivedAsync(eventArgs);

            char[] separators = { ',', ':' };
            sensor.Id = eventArgs.ApplicationMessage.Payload.ToString()
                .Split(separators, StringSplitOptions.RemoveEmptyEntries).GetValue(1).ToString();
            sensor.Temp = eventArgs.ApplicationMessage.Payload.ToString()
                .Split(separators, StringSplitOptions.RemoveEmptyEntries).GetValue(3).ToString();

            return _sensorService.CreateSensor(sensor);
        }

    }
}
