using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TORRES_backend._dataBind._bindHelper
{
    public class reportBind
    {
        public string email { get; set; }
        public string fullname { get; set; }
        public string bugCode { get; set; }
        public string bugTitle { get; set; }
        public string bugdescription { get; set; }
        public string bugLocation { get; set; }
        public string bugAttachedFile { get; set; }
        public DateTime startfixschedule { get; set; }
        public DateTime endfixschedule { get; set; }
        public char sendtodev { get; set; }
        public char devmaintenance { get; set; }
        public char bugstatus { get; set; }
        public DateTime createdAt { get; set; }
        public class initialStateReport
        {
            public reportBind reportObj;
            public void setObject(reportBind obj)
            {
                reportObj = obj;
            }
        }
    }
}