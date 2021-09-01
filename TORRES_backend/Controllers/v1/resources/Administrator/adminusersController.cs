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
using System.Web;
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
                    var checker = db.adminusers.Any(x => x.is_type == "1" && x.is_verified == "1");
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
        class adminClass
        {
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public char istype { get; set; }
            public char isverifier { get; set; }
            public DateTime createdat { get; set; }
        }
        class Response
        {
            public string message { get; set; }
        }
        Response resp = new Response();
        APISecurity apis = new APISecurity();
        adminClass admin = new adminClass();
        // POST: api/adminusers
        [Route("admin-store-data"), HttpPost]
        public IHttpActionResult Postadminuser()
        {
            try
            {
                var http = HttpContext.Current.Request;
                admin.firstname = http.Form["firstname"];
                admin.lastname = http.Form["lastname"];
                admin.email = http.Form["email"];
                admin.password = apis.Encrypt(http.Form["password"]);
                admin.istype = Convert.ToChar("1");
                admin.isverifier = Convert.ToChar("0");
                admin.createdat = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd h:m:s"));
                if(string.IsNullOrEmpty(admin.firstname) || string.IsNullOrEmpty(admin.lastname) || string.IsNullOrEmpty(admin.email) 
                    || string.IsNullOrEmpty(admin.password))
                {
                    resp.message = "empty";
                    return Ok(resp);
                }
                else
                {
                    using (db)
                    {
                        adminuser useradmin = new adminuser();
                        useradmin.firstname = admin.firstname;
                        useradmin.lastname = admin.lastname;
                        useradmin.email = admin.email;
                        useradmin.password = admin.password;
                        useradmin.is_type = Convert.ToString(admin.istype);
                        useradmin.is_verified = Convert.ToString(admin.isverifier);
                        useradmin.createdAt = admin.createdat;
                        useradmin.istoken = "";
                        db.adminusers.Add(useradmin);
                        db.SaveChanges();
                        resp.message = "success";
                        return Ok(resp);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
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