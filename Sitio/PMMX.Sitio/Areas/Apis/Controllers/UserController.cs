using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers
{
    public class UserController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/User
        public IQueryable<User> GetUser()
        {
            return db.Users;
        }

        // GET: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [ResponseType(typeof(UserView))]
        public IHttpActionResult ForgotPassword(string Email)
        {

            var id = db.Users.Where(x=> (x.Email.Equals(Email)))
                .Select(x=> x.Id)
                .FirstOrDefault();

            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Reset_password = true;
                user.Key_password = GetHashCode();
                    
                db.Entry(user).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(user);
                    }
                }
            }

            return Ok(user);
        }

        [HttpPost]
        [ResponseType(typeof(UserView))]
        public IHttpActionResult RessetPassword(string Email, string Password, int Key)
        {

            var id = db.Users.Where(x => (x.Email.Equals(Email)) && (x.Key_password.Equals(Key)))
                .Select(x => x.Id)
                .FirstOrDefault();

            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if(Key == user.Key_password)
                {
                    user.Reset_password = false;
                    user.Key_password = 0;
                    user.Password = Password;
                    db.Entry(user).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            return Ok(user);
                        }
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(user);
        }


        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/User
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

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

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}