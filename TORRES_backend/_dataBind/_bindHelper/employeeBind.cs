using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TORRES_backend._dataBind._bindHelper
{
    public class employeeBind
    {
        public string empname { get; set; }
        public string empemail { get; set; }
        public string emppassword { get; set; }
        public char apiaccess { get; set; }
        public string apikey { get; set; }
        public string grantAccess { get; set; }
        public DateTime createdAt { get; set; }

        public class employeeState
        {
            public employeeBind empBind;
            public void getState(employeeBind emp)
            {
                empBind = emp;
            }
        }
    }
}