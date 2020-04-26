using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cloud_API.Models
{
    public class BodyPartExercise
    {
        public int BodyPartId { get; set; }
        [JsonIgnore]
        public BodyPart BodyPart { get; set; }

        public int ExerciseId { get; set; }
        [JsonIgnore]
        public Exercise Exercise { get; set; }
    }
}
