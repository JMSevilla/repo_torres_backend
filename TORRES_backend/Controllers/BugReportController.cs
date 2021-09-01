using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using TORRES_backend.Models;

namespace TORRES_backend.Controllers
{
    [RoutePrefix("api/mr_report")]
    public class BugReportController : ApiController
    {
        class Response
        {
            public string message { get; set; }
        }
        Response res = new Response();
        private torresfullstackdbEntities core = new torresfullstackdbEntities();
        private report_a_bug bug = new report_a_bug();
        [Route("report-a-bug"), HttpPost]
        public IHttpActionResult reportbug()
        {
            try
            {
                var http = HttpContext.Current.Request;
                string email = http.Form["email"];
                string fullname = http.Form["fullname"];
                string bugCode = "0";
                string bugTitle = http.Form["bugTitle"];
                string bugdescription = http.Form["bugdescription"];
                string bugLocation = http.Form["bugLocation"];
                string bugAttachedFile = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.vectorstock.com%2Froyalty-free-vector%2Fisolated-website-code-and-bug-design-vector-28622335&psig=AOvVaw2AYcUBFamus5kCOuGvTsaf&ust=1630501405628000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCMDr4J-p2_ICFQAAAAAdAAAAABAD";
                DateTime startfixschedule = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd h:m:s"));
                DateTime endfixschedule = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd h:m:s"));
                char sendtodev = Convert.ToChar("0");
                char devmaintenance = Convert.ToChar("0");
                char bugstatus = Convert.ToChar("0");
                DateTime createdAt = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd h:m:s"));
                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullname) 
                    || string.IsNullOrEmpty(bugTitle) || string.IsNullOrEmpty(bugdescription) || string.IsNullOrEmpty(bugLocation))
                {
                    res.message = "required fields";
                    return Ok(res);
                }
                else
                {
                    using (core)
                    {
                        bug.clientemail = email;
                        bug.clientfullname = fullname;
                        bug.bugCode = bugCode;
                        bug.bugTitle = bugTitle;
                        bug.bugDescription = bugdescription;
                        bug.bugLocation = bugLocation;
                        bug.bugAttachedFile = bugAttachedFile;
                        bug.startfixschedule = startfixschedule;
                        bug.endfixschedule = endfixschedule;
                        bug.sendtodev = Convert.ToString(sendtodev);
                        bug.devmaintenance = Convert.ToString(devmaintenance);
                        bug.bugstatus = Convert.ToString(bugstatus);
                        bug.createdAt = createdAt;
                        core.report_a_bug.Add(bug);
                        core.SaveChanges();
                        res.message = "success";
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
