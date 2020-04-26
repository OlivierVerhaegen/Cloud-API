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

            // Opslaan van de data.
            //context.SaveChanges();
        }
    }
}
