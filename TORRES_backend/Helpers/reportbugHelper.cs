using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TORRES_backend._dataBind._bindHelper;
using TORRES_backend.Interfaces;
using static TORRES_backend._dataBind._bindHelper.reportBind;
using TORRES_backend.Models;
using TORRES_backend.APIConnection;

namespace TORRES_backend.Helpers
{
    public class reportbugHelper : ApiController, reportBugInterface
    {
        public static dynamic reportPool;
        public static string Reportmessage;
        dbtorresEntities core = Connection._publiccloud;
        class bugreportAppend
        {
            reportBind report = new reportBind();
            initialStateReport setval = new initialStateReport();
            report_a_bug reps = new report_a_bug();
            
            public void addReport()
            {
                var callback = HttpContext.Current.Request;
                report.email = callback.Form["email"];
                report.fullname = callback.Form["fullname"];
                report.bugCode = callback.Form["bugcode"];
                report.bugTitle = callback.Form["bugTitle"];
                report.bugdescription = callback.Form["bugdescription"];
                report.bugLocation = callback.Form["bugLocation"];
                report.bugAttachedFile = callback.Form["bugAttachedFile"];
                report.startfixschedule = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd h:m:s"));
                report.endfixschedule = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd h:m:s"));
                report.sendtodev = Convert.ToChar("0");
                report.devmaintenance = Convert.ToChar("0");
                report.bugstatus = Convert.ToChar("0");
                report.createdAt = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd h:m:s"));
                setval.setObject(report);
            }
            public void payload()
            {
                try
                {
                    reps.clientemail = setval.reportObj.email;
                    reps.clientfullname = setval.reportObj.fullname;
                    reps.bugCode = setval.reportObj.bugCode;
                    reps.bugTitle = setval.reportObj.bugTitle;
                    reps.bugDescription = setval.reportObj.bugdescription;
                    reps.bugLocation = setval.reportObj.bugLocation;
                    reps.bugAttachFile = setval.reportObj.bugAttachedFile;
                    reps.startfixschedule = setval.reportObj.startfixschedule;
                    reps.endfixschedule = setval.reportObj.endfixschedule;
                    reps.sendtodev = Convert.ToString(setval.reportObj.sendtodev);
                    reps.devmaintenance = Convert.ToString(setval.reportObj.devmaintenance);
                    reps.bugstatus = Convert.ToString(setval.reportObj.bugstatus);
                    reps.createdAt = setval.reportObj.createdAt;
                    reportPool = reps;
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        public IHttpActionResult do_addReport()
        {
            bugreportAppend append = new bugreportAppend();
            using (core)
            {
                append.addReport();
                append.payload();
                core.report_a_bug.Add(reportPool);
                core.SaveChanges();
                return Ok(Reportmessage);
            }
        }
    }
}