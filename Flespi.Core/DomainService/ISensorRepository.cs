using System.Collections.Generic;
using Flespi.Entity;

namespace Flespi.Core.DomainService
{
    public interface ISensorRepository
    {
        Sensor CreateSensor(Sensor newSensor);

        List<Sensor> GetAllSensors();

        Sensor GetSensorById(int id);
    }
}