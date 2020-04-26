using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cloud_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly DatabaseContext context;

        public ExercisesController(DatabaseContext context)
        {
            this.context = context;
        }

        // GET: api/v1/exercises
        [HttpGet]
        public ActionResult<IEnumerable<Exercise>> Get(string name, string sortBy, string direction, int page = 0, int limit = 10)
        {
            if (limit > 20)
                return BadRequest("Limit must be smaller then 20");

            IQueryable<Exercise> queryResult = context.Exercises.Include(e => e.TargetParts);

            if (!string.IsNullOrWhiteSpace(name))
                queryResult = queryResult.Where(e => e.Name == name);

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "name":
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(e => e.Name);
                        else
                            queryResult = queryResult.OrderByDescending(e => e.Name);
                        break;
                    default:
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(e => e.Id);
                        else
                            queryResult = queryResult.OrderByDescending(e => e.Id);
                        break;
                }
            }

            // Paging
            queryResult = queryResult.Skip(page * limit);
            queryResult = queryResult.Take(limit);

            return queryResult.ToList();
        }

        // GET: api/v1/exercises/{id}
        [HttpGet("{id}")]
        public ActionResult<Exercise> Get(int id)
        {
            Exercise e = context.Exercises
                .Include(e => e.TargetParts)
                .Single(e => e.Id == id);

            if (e == null)
                return NotFound();
            return e;
        }

        // POST: api/v1/exercises
        [HttpPost]
        public IActionResult Add([FromBody] Exercise e)
        {
            EntityEntry<Exercise> createdExercise = context.Exercises.Add(e);
            context.SaveChanges();

            return Created($"api/v1/users/{createdExercise.Entity.Id}", createdExercise.Entity);
        }

        // PUT: api/v1/exercises/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Exercise e)
        {
            Exercise exerciseToUpdate = context.Exercises.Find(id);

            if (exerciseToUpdate == null)
                return NotFound();

            exerciseToUpdate.Update(e);
            /* If the entry is being tracked, then invoking update API is not needed. 
              The API only needs to be invoked if the entry was not tracked. 
              https://www.learnentityframeworkcore.com/dbcontext/modifying-data */
            context.Exercises.Update(exerciseToUpdate);
            context.SaveChanges();

            return Ok();
        }

        // DELETE: api/v1/exercises/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Exercise exerciseToDelete = context.Exercises.Find(id);
            IEnumerable<BodyPartExercise> bpesToRemove = context.BodyPartExercises.Where(bpe => bpe.BodyPartId == id);

            if (exerciseToDelete == null)
                return NotFound();

            if (bpesToRemove != null && bpesToRemove.Any())
                context.BodyPartExercises.RemoveRange(bpesToRemove);

            context.Exercises.Remove(exerciseToDelete);
            context.SaveChanges();

            return Ok();
        }
    }
}