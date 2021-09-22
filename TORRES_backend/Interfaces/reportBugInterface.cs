using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace TORRES_backend.Interfaces
{
    public interface reportBugInterface
    {
        IHttpActionResult do_addReport();
    }
}
