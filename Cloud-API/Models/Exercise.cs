using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<BodyPartExercise> TargetParts { get; set; }

        [Required]
        public string Description { get; set; }

        [Url]
        public string VideoUrl { get; set; }

        public void Update(Exercise e)
        {
            this.Name = e.Name;
            this.VideoUrl = e.VideoUrl;
            this.Description = e.Description;

            if (e.TargetParts != null && e.TargetParts.Any())
            {
                if (this.TargetParts != null)
                {
                    foreach (BodyPartExercise bpe in e.TargetParts)
                    {
                        this.TargetParts.Add(bpe);
                    }
                } else
                {
                    this.TargetParts = e.TargetParts;
                }
            }
        }
    }
}
