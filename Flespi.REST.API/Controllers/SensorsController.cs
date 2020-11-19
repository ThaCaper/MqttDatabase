using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flespi.Core.AppService;
using Flespi.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flespi.REST.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;
        public SensorsController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }
        // GET: <SensorsController>
        [HttpGet]
        public ActionResult<IEnumerable<Sensor>> GetAllSensors()
        {
            return _sensorService.GetAllSensors();
        }

        // GET <SensorsController>/5
        [HttpGet("{id}")]
        public ActionResult<Sensor> GetSensor(int id)
        {
            return _sensorService.GetSensorById(id);
        }

        // POST <SensorsController>
        [HttpPost]
        public ActionResult<Sensor> Post([FromBody] Sensor newSensor)
        {
            return _sensorService.CreateSensor(newSensor);
        }

    }
}
