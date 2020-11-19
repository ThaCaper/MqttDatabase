using System.Collections.Generic;
using Flespi.Core.DomainService;
using Flespi.Entity;

namespace Flespi.Core.AppService.Impl
{
    public class SensorService : ISensorService
    {
        private ISensorRepository _sensorRepository;

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public Sensor CreateSensor(Sensor newSensor)
        {
            return _sensorRepository.CreateSensor(newSensor);
        }

        public List<Sensor> GetAllSensors()
        {
            return _sensorRepository.GetAllSensors();
        }

        public Sensor GetSensorById(int id)
        {
            return _sensorRepository.GetSensorById(id);
        }
    }
}