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
namespace TORRES_backend.Controllers.Signin
{
    [RoutePrefix("api/signin")]
    public class signinController : ApiController
    {
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
                var http = HttpContext.Current.Request;
                using (db)
                {
                    auth.email = http.Form["email"];
                    string pwd = apis.Encrypt(http.Form["password"]);
                    string encrypted = string.Empty;
                    string istype;
                    string isstatus;
                    var c1 = db.adminusers.Any(x => x.email == auth.email && x.is_verified != "0");
                    var c2 = db.adminusers.Where(x => x.email == auth.email).FirstOrDefault();
                    var c3 = db.users.Any(x => x.email == auth.email && x.is_archive != "1");
                    var c4 = db.users.Where(x => x.email == auth.email).FirstOrDefault();
                    if(string.IsNullOrEmpty(auth.email) || string.IsNullOrEmpty(pwd))
                    {
                        auth.status = "empty";
                        return Ok(auth);
                    }
                    else
                    {
                        if(c1)
                        {
                            encrypted = c2 == null ? "" : c2.password;
                            istype = c2.is_type;
                            isstatus = c2.is_verified;
                            string decryptoriginal = apis.Decrypt(pwd);
                            string decryptrequest = apis.Decrypt(encrypted);
                            if( decryptrequest == decryptoriginal )
                            {
                                if(isstatus == "1")
                                {
                                    if(istype == "1" )
                                    {
                                        //admin
                                        var getdata = db.adminusers.Where(x => x.email == auth.email).Select(t => new
                                        {
                                            t.adminID,
                                            t.firstname,
                                            t.lastname,
                                            t.is_verified
                                        }).ToList();
                                        auth.databulk = getdata.FirstOrDefault();
                                        auth.status = "SUCCESS ADMIN";
                                        return Ok(auth);
                                    }
                                    else
                                    {
                                        return Ok("no user");
                                    }
                                }
                                else
                                {
                                    return Ok("account disabled");
                                }
                            }
                            else
                            {
                                return Ok("wrong password");
                            }
                        }
                        else if (c3)
                        {
                            encrypted = c4 == null ? "" : c4.password;
                            istype = c4.is_type;
                            isstatus = c4.is_activate;
                            string decryptoriginal = apis.Decrypt(pwd);
                            string decryptrequest = apis.Decrypt(encrypted);
                            if(decryptrequest == decryptoriginal)
                            {
                                if (isstatus == "1")
                                {
                                    if (istype == "1")
                                    {
                                        var getdata = db.users.Where(x => x.email == auth.email).Select(t => new
                                        {
                                           t.client_id,
                                           t.firstname,
                                           t.lastname,
                                           t.is_type, t.image_url
                                        }).ToList();
                                        auth.databulk = getdata.FirstOrDefault();
                                        auth.status = "SUCCESS";
                                        return Ok(auth);
                                    }
                                    else
                                    {
                                        return Ok("no user");
                                    }
                                }
                                else
                                {
                                    return Ok("account disabled");
                                }
                            }
                            else
                            {
                                return Ok("wrong password");
                            }
                        }
                        else
                        {
                            return Ok("not found");
                        }
                    }
                }
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
                using (db)
                {
                    var check = db.adminusers.Where(x => x.email == email).FirstOrDefault();
                    if(check != null)
                    {
                        check.istoken = token;
                        db.SaveChanges();
                        auth.status = "update token admin";
                        auth.databulk = db.adminusers.Where(x => x.email == email).Select(t => new
                        {
                            t.istoken
                        }).ToList();
                        return Ok(auth);
                    }
                    return Ok();
                }
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