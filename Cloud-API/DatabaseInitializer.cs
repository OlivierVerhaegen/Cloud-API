using Cloud_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_API
{
    public class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            // Hier kan men initiele data toevoegen.
            if (!context.Users.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    User u = new User()
                    {
                        Name = $"User {i}",
                        Email = $"user{i}@mail.com",
                        BirthDate = new DateTime(1900 + i, 10, 12)
                    };

                    context.Users.Add(u);
                }
            }

            // Opslaan van de data.
            context.SaveChanges();
        }
    }
}
