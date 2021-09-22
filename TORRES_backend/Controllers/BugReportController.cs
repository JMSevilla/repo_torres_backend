using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using TORRES_backend.Models;
using TORRES_backend.Helpers;

namespace TORRES_backend.Controllers
{
    [RoutePrefix("api/mr_report")]
    public class BugReportController : ApiController
    {
        reportbugHelper apiIntegration = new reportbugHelper();
        [Route("report-a-bug"), HttpPost]
        public IHttpActionResult reportbug()
        {
            try
            {
                apiIntegration.do_addReport();
                return Ok(reportbugHelper.Reportmessage);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
