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
using TORRES_backend.Models;

namespace TORRES_backend.Controllers
{
    [RoutePrefix("api/administrator")]
    public class adminusersController : ApiController
    {
        private torresfullstackdbEntities db = new torresfullstackdbEntities();

        // GET: api/adminusers
        public IQueryable<adminuser> Getadminusers()
        {
            return db.adminusers;
        }

        // GET: api/adminusers/5
        [Route("detect-admin-registration"), HttpGet]
        public IHttpActionResult Getadminuser()
        {
            try
            {
                using (db)
                {
                    var checker = db.adminusers.Any(x => x.is_type == "1");
                    if (checker)
                    {
                        return Ok("exist");
                    }
                    else
                    {
                        return Ok("not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/adminusers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putadminuser(int id, adminuser adminuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminuser.adminID)
            {
                return BadRequest();
            }

            db.Entry(adminuser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!adminuserExists(id))
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

        // POST: api/adminusers
        [ResponseType(typeof(adminuser))]
        public IHttpActionResult Postadminuser(adminuser adminuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.adminusers.Add(adminuser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adminuser.adminID }, adminuser);
        }

        // DELETE: api/adminusers/5
        [ResponseType(typeof(adminuser))]
        public IHttpActionResult Deleteadminuser(int id)
        {
            adminuser adminuser = db.adminusers.Find(id);
            if (adminuser == null)
            {
                return NotFound();
            }

            db.adminusers.Remove(adminuser);
            db.SaveChanges();

            return Ok(adminuser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool adminuserExists(int id)
        {
            return db.adminusers.Count(e => e.adminID == id) > 0;
        }
    }
}