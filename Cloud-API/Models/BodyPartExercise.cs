using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_API.Models
{
    public class BodyPartExercise
    {
        public int BodyPartId { get; set; }
        public BodyPart BodyPart { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
