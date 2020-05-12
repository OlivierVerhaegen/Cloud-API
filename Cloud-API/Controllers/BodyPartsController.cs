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
    public class BodyPartsController : ControllerBase
    {
        private readonly DatabaseContext context;

        public BodyPartsController(DatabaseContext context)
        {
            this.context = context;
        }

        // GET: api/v1/bodyparts
        [HttpGet]
        public ActionResult<IEnumerable<BodyPart>> Get(string name, string location, string sortBy, string direction, int page = 0, int limit = 10)
        {
            if (limit > 20)
                return BadRequest("Limit must be smaller then 20");

            IQueryable<BodyPart> queryResult = context.BodyParts.Include(bp => bp.Exercises);

            if (!string.IsNullOrWhiteSpace(name))
                queryResult = queryResult.Where(bp => bp.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(location))
                queryResult = queryResult.Where(bp => bp.Location.Contains(location));

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "name":
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(bp => bp.Name);
                        else
                            queryResult = queryResult.OrderByDescending(bp => bp.Name);
                        break;
                    case "location":
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(bp => bp.Location);
                        else
                            queryResult = queryResult.OrderByDescending(bp => bp.Location);
                        break;
                    default:
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(bp => bp.Id);
                        else
                            queryResult = queryResult.OrderByDescending(bp => bp.Id);
                        break;
                }
            }

            // Paging
            queryResult = queryResult.Skip(page * limit);
            queryResult = queryResult.Take(limit);

            return queryResult.ToList();
        }

        // GET: api/v1/bodyparts/{id}
        [HttpGet("{id}")]
        public ActionResult<BodyPart> Get(int id)
        {
            BodyPart bp = context.BodyParts
                .Include(bp => bp.Exercises)
                .Single(bp => bp.Id == id);

            if (bp == null)
                return NotFound();
            return bp;
        }

        // POST: api/v1/bodyparts
        [HttpPost]
        public IActionResult Add([FromBody] BodyPart bp)
        {
            EntityEntry<BodyPart> createdBodyPart = context.BodyParts.Add(bp);
            context.SaveChanges();

            return Created($"api/v1/users/{createdBodyPart.Entity.Id}", createdBodyPart.Entity);
        }

        // PUT: api/v1/bodyparts/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BodyPart bp)
        {
            BodyPart bodyPartToUpdate = context.BodyParts.Find(id);

            if (bodyPartToUpdate == null)
                return NotFound();

            bodyPartToUpdate.Update(bp);
            /* If the entry is being tracked, then invoking update API is not needed. 
              The API only needs to be invoked if the entry was not tracked. 
              https://www.learnentityframeworkcore.com/dbcontext/modifying-data */
            context.BodyParts.Update(bodyPartToUpdate);
            context.SaveChanges();

            return Ok();
        }

        // DELETE: api/v1/bodyparts/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            BodyPart bodyPartToDelete = context.BodyParts.Find(id);
            IEnumerable<BodyPartExercise> bpesToRemove = context.BodyPartExercises.Where(bpe => bpe.BodyPartId == id);

            if (bodyPartToDelete == null)
                return NotFound();

            if (bpesToRemove != null && bpesToRemove.Any())
                context.BodyPartExercises.RemoveRange(bpesToRemove);

            context.BodyParts.Remove(bodyPartToDelete);
            context.SaveChanges();

            return Ok();
        }
    }
}