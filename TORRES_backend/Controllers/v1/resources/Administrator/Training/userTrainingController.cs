using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TORRES_backend.Models;
using System.Web;
namespace TORRES_backend.Controllers.v1.resources.Administrator.Training
{
    [RoutePrefix("api/v1/resources/training")]
    public class userTrainingController : ApiController
    {
        private torresfullstackdbEntities db = new torresfullstackdbEntities();

        class Response
        {
            public string message { get; set; }
        }
        class tValues
        {
            public char isonline { get; set; }
            public string trainingName { get; set; }
            public string SD { get; set; }
            public string FD { get; set; }
            public string WYL { get; set; }
            public string imageURL { get; set; }
            public char isstatus { get; set; }
            public char isforum { get; set; }
            public char islivechat { get; set; }
            public int capacity { get; set; }
            public char ispayment { get; set; }
            public decimal coursefee { get; set; }
            public string effort { get; set; }
            public string tLength { get; set; }
            public string categories { get; set; }
            public DateTime trainingStart { get; set; }
            public DateTime trainingEnd { get; set; }
            public string empAssign { get; set; }
            public DateTime createdAt { get; set; }
        }
        tValues t = new tValues();
        Response resp = new Response();
        [Route("add-training"), HttpPost]
        public IHttpActionResult addTraining()
        {
            var server = HttpContext.Current.Request;
            t.isonline = Convert.ToChar(server.Form["isonline"]);
            t.trainingName = server.Form["trainingName"];
            t.SD = server.Form["SD"];
            t.FD = server.Form["FD"];
            t.WYL = server.Form["WYL"];
            t.imageURL = server.Form["imageurl"];
            t.isstatus = Convert.ToChar(server.Form["isstatus"]);
            t.isforum = Convert.ToChar(server.Form["isforum"]);
            t.islivechat = Convert.ToChar(server.Form["islivechat"]);
            t.capacity = Convert.ToInt32(server.Form["capacity"]);
            t.ispayment = Convert.ToChar(server.Form["ispayment"]);
            t.coursefee = Convert.ToDecimal(server.Form["coursefee"]);
            t.effort = server.Form["effort"];
            t.tLength = server.Form["tLength"];
            t.categories = server.Form["categories"];
            t.trainingStart = Convert.ToDateTime(server.Form["trainingStart"]);
            t.trainingEnd = Convert.ToDateTime(server.Form["trainingEnd"]);
            t.empAssign = server.Form["assignee"];
            t.createdAt = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd"));
             dataTraining(
                 t.isonline, t.trainingName, t.SD, t.FD, t.WYL, t.imageURL,
                 t.isstatus, t.isforum, t.islivechat, t.capacity,
                 t.ispayment, t.coursefee, t.effort, t.tLength,
                 t.categories, t.trainingStart, t.trainingEnd,
                 t.empAssign, t.createdAt
                 );
            resp.message = "SUCCESS CREATE TRAINING";
            return Ok(resp);
        }
        public HttpResponseMessage dataTraining(
            char isonline, string trainingName, string SD, string FD, string WYL,
            string imageURL, char istatus, char isforum, char islivechat, int capacity,
            char ispayment, decimal coursefee, string effort, string tlength, 
            string categories, DateTime trainingStart, DateTime trainingEnd, string assignee,
            DateTime createdAt
            )
        {
            using (db)
            {
                training train = new training();
                train.isonline = Convert.ToString(isonline);
                train.trainingName = trainingName;
                train.SD = SD;
                train.FD = FD;
                train.WYL = WYL;
                train.imageURL = imageURL;
                train.isstatus = Convert.ToString(istatus);
                train.isforum = Convert.ToString(isforum);
                train.islivechat = Convert.ToString(islivechat);
                train.capacity = capacity;
                train.ispayment = Convert.ToString(ispayment);
                train.coursefee = coursefee;
                train.effort = effort;
                train.tLength = tlength;
                train.categories = categories;
                train.trainingStart = trainingStart;
                train.trainingEnd = trainingEnd;
                train.empAssignee = assignee;
                train.createdAt = createdAt;
                
                db.trainings.Add(train);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
