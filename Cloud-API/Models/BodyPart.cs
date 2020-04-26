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
    }
}
