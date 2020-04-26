using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Max. 32 characters for a name.")]
        public string Name { get; set; }


        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(0, 10)]
        public int? NumberOfExercises { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

        public ICollection<BodyPart> TartgetParts { get; set; }

        public void Update(User u)
        {
            this.Name = u.Name;
            this.Email = u.Email;
            this.BirthDate = u.BirthDate;

            if (u.TartgetParts != null && u.Exercises.Any())
            {
                if (this.Exercises != null)
                {
                    foreach (Exercise e in u.Exercises)
                    {
                        this.Exercises.Add(e);
                    }
                } else
                {
                    this.Exercises = u.Exercises;
                }
            }

            if (u.TartgetParts != null && u.TartgetParts.Any())
            {
                if (this.TartgetParts != null)
                {
                    foreach (BodyPart bp in u.TartgetParts)
                    {
                        this.TartgetParts.Add(bp);
                    }
                } else
                {
                    this.TartgetParts = u.TartgetParts;
                }
            }
        }
    }
}
