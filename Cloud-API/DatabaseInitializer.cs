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

            // Default Lichaamsdelen aanmaken.
            if (!context.BodyParts.Any())
            {
                BodyPart bicep = new BodyPart()
                {
                    Name = "Bicep",
                    Location = "Bovenarm",
                    ImageUrl = "https://img.webmd.com/dtmcms/live/webmd/consumer_assets/site_images/articles/image_article_collections/anatomy_pages/493x335_bicep.jpg?resize=646px:*&output-quality=100"
                };

                context.BodyParts.Add(bicep);

                BodyPart bilspier = new BodyPart()
                {
                    Name = "Bilspier",
                    Location = "Bil",
                    ImageUrl = "https://www.pt-nijmegen.nl/images/2_bilspier_2.gif"
                };

                context.BodyParts.Add(bilspier);

                BodyPart buikspieren = new BodyPart()
                {
                    Name = "Buikspieren",
                    Location = "Buik",
                    ImageUrl = "https://dfc.nl/wp-content/upload_folders/dfc.nl/buikspieren-anatomie-1024x1024.jpg"
                };

                context.BodyParts.Add(buikspieren);
            }

            // Default oefeningen aanmaken.
            if (!context.Exercises.Any())
            {
                Exercise squat = new Exercise()
                {
                    Name = "Squat",
                    Description = "Trainen van de beenspieren...",
                    VideoUrl = "https://www.youtube.com/watch?v=U3HlEF_E9fo"
                };

                context.Exercises.Add(squat);

                Exercise sitUp = new Exercise()
                {
                    Name = "Sit-up",
                    Description = "Trainen van de buikspieren ...",
                    VideoUrl = "https://www.youtube.com/watch?v=jDwoBqPH0jk"
                };

                context.Exercises.Add(sitUp);

                Exercise barbellCurl = new Exercise()
                {
                    Name = "Barbel Curl",
                    Description = "Trainen van de bicep ...",
                    VideoUrl = "https://www.youtube.com/watch?v=ykJmrZ5v0Oo"
                };

                context.Exercises.Add(barbellCurl);
            }

            // Default users aanmaken.
            if (!context.Users.Any())
            {
                User u = new User()
                {
                    Name = "Jos",
                    Email = "jos@mail.com",
                    BirthDate = new DateTime(1980, 10, 12)
                };

                context.Users.Add(u);

                User u1 = new User()
                {
                    Name = "Frank",
                    Email = "frank@mail.com",
                    BirthDate = new DateTime(1985, 10, 12)
                };

                context.Users.Add(u1);

                User u2 = new User()
                {
                    Name = "Tom",
                    Email = "tom@mail.com",
                    BirthDate = new DateTime(1990, 10, 12)
                };

                context.Users.Add(u2);
            }

            // Opslaan van de data.
            context.SaveChanges();
        }
    }
}
