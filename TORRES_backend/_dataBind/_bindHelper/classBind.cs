using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TORRES_backend._dataBind._bindHelper
{
    public class classBind
    {
        public String classname { get; set; }
        public String trainingUnder { get; set; }
        public String status { get; set; }
        public String description { get; set; }
        public DateTime createdAt { get; set; }

        public class classState
        {
            public classBind classBind;
            public void getState(classBind classBind)
            {
                this.classBind = classBind;
            }
        }
    }
}