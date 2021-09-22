using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TORRES_backend.Interfaces;
using TORRES_backend.Models;
using TORRES_backend._dataBind._bindHelper;

namespace TORRES_backend.Helpers
{
    public class TrainingAPIHelper : ApiController, TrainingInterface
    {
        private torresfullstackdbEntities db = new torresfullstackdbEntities();
        
       
        public static string message;
        public static dynamic responsePool;
        public static dynamic trainingArray;
         class trainingAppend
        {
            trainingBindHelper t = new trainingBindHelper();
            trainingHelperInitiateVal setval = new trainingHelperInitiateVal();
            training _entityTrain = new training();
            public void _shifts()
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
                setval.setObject(t);
            }
            public void Saved()
            {
                if (_entityTrain != null)
                {
                    _entityTrain.isonline = Convert.ToString(setval.trainingObj.isonline);
                    _entityTrain.trainingName = setval.trainingObj.trainingName;
                    _entityTrain.SD = setval.trainingObj.SD;
                    _entityTrain.FD = setval.trainingObj.FD;
                    _entityTrain.WYL = setval.trainingObj.WYL;
                    _entityTrain.imageURL = setval.trainingObj.imageURL;
                    _entityTrain.isstatus = Convert.ToString(setval.trainingObj.isstatus);
                    _entityTrain.isforum = Convert.ToString(setval.trainingObj.isforum);
                    _entityTrain.islivechat = Convert.ToString(setval.trainingObj.islivechat);
                    _entityTrain.capacity = setval.trainingObj.capacity;
                    _entityTrain.ispayment = Convert.ToString(setval.trainingObj.ispayment);
                    _entityTrain.coursefee = setval.trainingObj.coursefee;
                    _entityTrain.effort = setval.trainingObj.effort;
                    _entityTrain.tLength = setval.trainingObj.tLength;
                    _entityTrain.categories = setval.trainingObj.categories;
                    _entityTrain.trainingStart = setval.trainingObj.trainingStart;
                    _entityTrain.trainingEnd = setval.trainingObj.trainingEnd;
                    _entityTrain.empAssignee = setval.trainingObj.empAssign;
                    _entityTrain.createdAt = setval.trainingObj.createdAt;
                    responsePool = _entityTrain;
                }
            }
        }

        public IHttpActionResult BETraining()
        {
            trainingAppend appendTraining = new trainingAppend();
            using (db)
            {
                appendTraining._shifts();
                appendTraining.Saved();
                db.trainings.Add(responsePool);
                db.SaveChanges();
                message = "SUCCESS CREATE TRAINING";
                return Ok(message);
            }
        }

        public IHttpActionResult BEFetchAllTraining()
        {
            try
            {
                trainingArray = db.trainings.ToList();
                return Ok(trainingArray);
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}