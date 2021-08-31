using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DemoWb.Models;

namespace DemoWb.Controllers
{
    public class UsersController : ApiController
    {
        private DemoDatebaseEntities2 db = new DemoDatebaseEntities2();

        // GET: api/Users
        [Route("api/users")]
        public IEnumerable<user> GetUsers()
        {
            return db.users.ToList();
        }

        // GET: api/Users/5
        [Route("api/users/checkuser")]
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> PostUS(user users)
        {
            var list = db.users.ToList();
            foreach(var item in list)
            {
                if(item.name == users.name && item.password == users.password)
                {
                    return StatusCode(HttpStatusCode.OK);
                }
            }
            return NotFound();
        }


        // PUT: api/Users/5
        [Route("api/users/putuser")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Users
        [Route("api/users/postusers")]
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> PostUser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/Users/5
        [Route("api/users/deleteuser")]
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            user user = await db.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.users.Count(e => e.id == id) > 0;
        }
    }
}