using System.Collections.Generic;
using System.Linq;
using Flespi.Core.DomainService;
using Flespi.Entity;
using Microsoft.EntityFrameworkCore;

namespace Flespi.Infrastructure.SQL.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly DatabaseContext _context;

        public SensorRepository(DatabaseContext context)
        {
            _context = context;
        }
        public Sensor CreateSensor(Sensor newSensor)
        {
            _context.Sensors.Add(newSensor).State = EntityState.Added;
            _context.SaveChanges();
            return newSensor;
        }

        public List<Sensor> GetAllSensors()
        {
            return _context.Sensors.AsNoTracking().ToList();
        }

        public Sensor GetSensorById(int id)
        {
            return _context.Sensors.AsNoTracking().FirstOrDefault(s => s.Id == id);
        }
    }
}