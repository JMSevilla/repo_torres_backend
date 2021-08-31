using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TORRES_backend.Models;
using System.Web;
namespace TORRES_backend.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        private torresfullstackdbEntities core = new torresfullstackdbEntities();
        private StudentClass add_stud = new StudentClass();
        private Response res = new Response();
        private user tbuser = new user();
        APISecurity apis = new APISecurity();
        [Route("add-student")]
        public IHttpActionResult StudentRegistration()
        {
            try
            {
                var http = HttpContext.Current.Request;
                add_stud.firstname = http.Form["firstname"];
                add_stud.lastname = http.Form["lastname"];
                add_stud.birthdate = Convert.ToDateTime(http.Form["birthdate"]);
                add_stud.age = Convert.ToInt32(http.Form["age"]);
                add_stud.gender = Convert.ToChar(http.Form["gender"]);
                add_stud.contactnum = http.Form["contactnum"];
                add_stud.province = http.Form["province"];
                add_stud.municipality = http.Form["municipality"];
                add_stud.zip_code = http.Form["zip_code"];
                add_stud.address = http.Form["address"];
                add_stud.email = http.Form["email"];
                add_stud.password = apis.Encrypt(http.Form["password"]);
                add_stud.image_url = http.Form["image_url"];
                add_stud.is_type = Convert.ToChar("3");
                add_stud.is_activate = Convert.ToChar("1");
                add_stud.is_token_verified = Convert.ToChar("0");
                add_stud.is_archive = Convert.ToChar("0");
                add_stud.createdAt = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd h:m:s"));
                if(string.IsNullOrEmpty(add_stud.firstname) || string.IsNullOrEmpty(add_stud.lastname)
                    || string.IsNullOrEmpty(add_stud.email) || string.IsNullOrEmpty(add_stud.password))
                {
                    res.message = "Required fields";
                    return Ok(res);
                }
                else
                {
                    using(core)
                    {
                        if(core.users.Any(x => x.email == add_stud.email))
                        {
                            res.message = "Username already exists";
                            return Ok(res);
                        }
                        else
                        {
                            tbuser.firstname = add_stud.firstname;
                            tbuser.lastname = add_stud.lastname;
                            tbuser.birthdate = add_stud.birthdate;
                            tbuser.age = add_stud.age;
                            tbuser.gender = Convert.ToString(add_stud.gender);
                            tbuser.contactnum = add_stud.contactnum;
                            tbuser.province = add_stud.province;
                            tbuser.municipality = add_stud.municipality;
                            tbuser.zip_code = add_stud.zip_code;
                            tbuser.address = add_stud.address;
                            tbuser.email = add_stud.email;
                            tbuser.password = add_stud.password;
                            tbuser.image_url = add_stud.image_url;
                            tbuser.is_type = Convert.ToString(add_stud.is_type);
                            tbuser.is_activate = Convert.ToString(add_stud.is_activate);
                            tbuser.is_token_verified = Convert.ToString(add_stud.is_token_verified);
                            tbuser.is_archive = Convert.ToString(add_stud.is_archive);
                            tbuser.createdAt = add_stud.createdAt;
                            tbuser.istoken = "";
                            core.users.Add(tbuser);
                            core.SaveChanges();
                            res.message = "success";
                            return Ok(res);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("student-classcode"), HttpPost]
        public IHttpActionResult scan_classcode()
        {
            try
            {
                var http = HttpContext.Current.Request;
                add_stud.classcode = http.Form["classcode"];
                using (core)
                {
                    if(core.class_code_tb.Any(x => x.classcode == add_stud.classcode))
                    {
                        res.message = "success";
                        return Ok(res);
                    }
                    else
                    {
                        res.message = "Invalid class code";
                        return Ok(res);
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
