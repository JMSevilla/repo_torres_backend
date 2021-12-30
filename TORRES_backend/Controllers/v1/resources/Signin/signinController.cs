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
using TORRES_backend.Helpers;
using System.Web;
namespace TORRES_backend.Controllers.Signin
{
    [RoutePrefix("api/v1/resources/signin")]
    public class signinController : ApiController
    {
        credentialsHelper __helper = new credentialsHelper();
        private torresfullstackdbEntities db = new torresfullstackdbEntities();
        APISecurity apis = new APISecurity();
        // GET: api/signin
        public IQueryable<adminuser> Getadminusers()
        {
            return db.adminusers;
        }

        // GET: api/signin/5
        [ResponseType(typeof(adminuser))]
        public IHttpActionResult Getadminuser(int id)
        {
            adminuser adminuser = db.adminusers.Find(id);
            if (adminuser == null)
            {
                return NotFound();
            }

            return Ok(adminuser);
        }

        // PUT: api/signin/5
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
       class authcredentials
        {
            public string email { get; set; }
            public string password { get; set; }
            public object databulk { get; set; }
            public string status { get; set; }
        }
        authcredentials auth = new authcredentials();
        
        // POST: api/signin
        [Route("standard-login"), HttpPost]
        public IHttpActionResult Postadminuser()
        {
            try
            {
                __helper.loginInterface();
                return Ok(credentialsHelper.getSigninResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("update-token-admin"), HttpPut]
        public IHttpActionResult updatetoken(string email, string token)
        {
            try
            {
                __helper.tokenInterface(email, token);
                return Ok(credentialsHelper.getSigninResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("update-destroy-token")]
        [HttpPut]
        public IHttpActionResult destroyToken(string email)
        {
            try
            {
                __helper.tokenDestroyInterface(email);
                return Ok(credentialsHelper.doneLogout);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("check-token"), HttpGet]
        public IHttpActionResult getchecktoken(string token, string email)
        {
            try
            {
                using (db)
                {
                    if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                    {
                        return Ok("no data");
                    }
                    else
                    {
                        var admin_check_token = db.adminusers.Any(x => x.email == email && x.istoken == token);
                        var normal_user_token = db.users.Any(x => x.email == email && x.istoken == token);
                        if (admin_check_token)
                        {
                            return Ok("admin direct");
                        }
                        else if (normal_user_token)
                        {
                            return Ok("user direct");
                        }
                        else
                        {
                            return Ok("home direct");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE: api/signin/5
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