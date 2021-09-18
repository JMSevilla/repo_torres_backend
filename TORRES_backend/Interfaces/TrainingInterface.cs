using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using TORRES_backend.Models;
using System.Web.Http;

namespace TORRES_backend.Interfaces
{
    public interface TrainingInterface
    {
       
        IHttpActionResult BETraining();
    }
}
