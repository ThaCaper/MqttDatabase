using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Flespi.Entity;
using Microsoft.EntityFrameworkCore.Internal;

namespace Flespi.Infrastructure.SQL.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        public void Initialize(DatabaseContext context)
        {
            SeedDb(context);
        }

        private void SeedDb(DatabaseContext ctx)
        {
            // Create the database, if it does not already exists. If the database
            // already exists, no action is taken (and no effort is made to ensure it
            // is compatible with the model for this context).
            ctx.Database.EnsureCreated();

            if (ctx.Sensors.Any())
            {
                return;
            }

            List<Sensor> sensors = new List<Sensor>
            {
                new Sensor
                {
                    Id = "1",
                    Temp = "21.21"
                },
                new Sensor
                {
                    Id = "2",
                    Temp = "22.22"
                },
                new Sensor
                {
                    Id = "3",
                    Temp = "23.23"
                },
                new Sensor
                {
                    Id = "4",
                    Temp = "24.24"
                },
                new Sensor
                {
                    Id = "5",
                    Temp = "25.25"
                },
                new Sensor
                {
                    Id = "6",
                    Temp = "26.26"
                },
                new Sensor
                {
                    Id = "7",
                    Temp = "20.20"
                },
                new Sensor
                {
                    Id = "8",
                    Temp = "21.00"
                },
                new Sensor
                {
                    Id = "9",
                    Temp = "22.40"
                },
                new Sensor
                {
                    Id = "10",
                    Temp = "20.20"
                },
            };

            List<User> users = new List<User>
            {
                new User 
                {
                    UserName = "UserJoe"
                },
                new User 
                {
                    UserName = "AdminAnn"
                }
            };

            ctx.Sensors.AddRange(sensors);
            ctx.Users.AddRange(users);
            ctx.SaveChanges();
        }
    }
}