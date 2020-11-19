using System.Collections.Generic;
using Flespi.Entity;

namespace Flespi.Core.AppService
{
    public interface ISensorService
    {
        Sensor CreateSensor(Sensor newSensor);

        List<Sensor> GetAllSensors();

        Sensor GetSensorById(int id);
    }
}