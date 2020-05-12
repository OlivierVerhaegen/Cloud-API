using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cloud_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext context;

        public UsersController(DatabaseContext context)
        {
            this.context = context;
        }

        // GET: api/v1/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get(string name, string email, string sortBy, string direction, int page = 0, int limit = 10, DateTime? birthDate = null)
        {
            if (limit > 20)
                return BadRequest("Limit must be smaller then 20");

            IQueryable<User> queryResult = context.Users.Include(u => u.Exercises).Include(u => u.TartgetParts);

            if (!string.IsNullOrWhiteSpace(name))
                queryResult = queryResult.Where(u => u.Name == name);
            if (birthDate != null)
                queryResult = queryResult.Where(u => u.BirthDate == birthDate);
            if (!string.IsNullOrWhiteSpace(email))
                queryResult = queryResult.Where(u => u.Email == email);

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch(sortBy)
                {
                    case "name":
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(u => u.Name);
                        else
                            queryResult = queryResult.OrderByDescending(u => u.Name);
                        break;
                    case "birthdate":
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(u => u.BirthDate);
                        else
                            queryResult = queryResult.OrderByDescending(u => u.BirthDate);
                        break;
                    case "email":
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(u => u.Email);
                        else
                            queryResult = queryResult.OrderByDescending(u => u.Email);
                        break;
                    default:
                        if (direction == "asc")
                            queryResult = queryResult.OrderBy(u => u.Id);
                        else
                            queryResult = queryResult.OrderByDescending(u => u.Id);
                        break;
                }
            }

            // Paging
            queryResult = queryResult.Skip(page * limit);
            queryResult = queryResult.Take(limit);
            
            return queryResult.ToList();
        }

        // GET: api/v1/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User u = context.Users.Find(id);

            if (u == null)
                return NotFound();
            return u;
        }

        // POST: api/v1/users
        [HttpPost]
        public IActionResult Add([FromBody] User u)
        {
            EntityEntry<User> createdUser = context.Users.Add(u);
            context.SaveChanges();

            return Created($"api/v1/users/{createdUser.Entity.Id}", createdUser.Entity);
        }

        // PUT: api/v1/users/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User u)
        {
            User userToUpdate = context.Users.Find(id);

            if (userToUpdate == null)
                return NotFound();

            userToUpdate.Update(u);
            /* If the entry is being tracked, then invoking update API is not needed. 
              The API only needs to be invoked if the entry was not tracked. 
              https://www.learnentityframeworkcore.com/dbcontext/modifying-data */
            context.Users.Update(userToUpdate);
            context.SaveChanges();

            return Ok();
        }

        // DELETE: api/v1/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User userToDelete = context.Users.Find(id);

            if (userToDelete == null)
                return NotFound();

            context.Users.Remove(userToDelete);
            context.SaveChanges();

            return Ok();
        }
    }
}