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
        public string Name { get; set; }


        public DateTime BirthDate { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

        public ICollection<BodyPart> TartgetParts { get; set; }
    }
}
