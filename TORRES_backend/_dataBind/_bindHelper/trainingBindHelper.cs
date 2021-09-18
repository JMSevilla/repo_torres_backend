using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TORRES_backend._dataBind._bindHelper
{
    public class trainingBindHelper
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
    public class trainingHelperInitiateVal
    {
        public trainingBindHelper trainingObj;
        public void setObject(trainingBindHelper obj)
        {
            trainingObj = obj;
        }
    }
}