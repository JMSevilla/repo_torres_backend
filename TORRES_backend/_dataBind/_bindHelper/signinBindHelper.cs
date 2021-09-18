using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TORRES_backend._dataBind._bindHelper
{
    public class signinBindHelper
    {
        public string email { get; set; }
        public string password { get; set; }
        public string _encrypted { get; set; }
        public string istype { get; set; }
        public string isstatus { get; set; }
        public class initialState
        {
            public signinBindHelper signObj;
            public void setObject(signinBindHelper obj)
            {
                signObj = obj;
            }
        }
    }
    public class tokenBindHelper
    {
        public string email { get; set; }
        public string token { get; set; }
        public class initialtokenState
        {
            public tokenBindHelper signObj;
            public void setObject(tokenBindHelper obj)
            {
                signObj = obj;
            }
        }
    }
}