using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TORRES_backend.Models;
using System.Web;
using TORRES_backend.Helpers;
namespace TORRES_backend.Controllers.v1.resources.Administrator.Training
{
    [RoutePrefix("api/v1/resources/training")]
    public class userTrainingController : ApiController
    {
        TrainingAPIHelper __helper = new TrainingAPIHelper();
        [Route("add-training"), HttpPost]
        public IHttpActionResult addTraining()
        {
            __helper.BETraining();
            return Ok(TrainingAPIHelper.message);
        }
    }
}
