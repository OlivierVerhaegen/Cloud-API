using System;
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
    }
}
