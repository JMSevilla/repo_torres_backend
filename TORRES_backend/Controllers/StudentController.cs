using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TORRES_backend.Models;
using System.Web;
using TORRES_backend.Helpers;

namespace TORRES_backend.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        private torresfullstackdbEntities core = new torresfullstackdbEntities();
        private StudentClass add_stud = new StudentClass();
        private Response res = new Response();
        studentHelper __helper = new studentHelper();
        [Route("add-student"), HttpPost]
        public IHttpActionResult StudentRegistration()
        {
            try
            {
                __helper.addStudent();
                return Ok(studentHelper.studResp);
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
