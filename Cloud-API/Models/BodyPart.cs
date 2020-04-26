using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_API.Models
{
    public class BodyPart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public ICollection<BodyPartExercise> Exercises { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public void Update(BodyPart bp)
        {
            this.Name = bp.Name;
            this.Location = bp.Location;
            this.ImageUrl = bp.ImageUrl;

            if (bp.Exercises != null && bp.Exercises.Any())
            {
                if (this.Exercises != null)
                {
                    foreach (BodyPartExercise bpe in bp.Exercises)
                    {
                        this.Exercises.Add(bpe);
                    }
                } else
                {
                    this.Exercises = bp.Exercises;
                }
            }
        }
    }
}
