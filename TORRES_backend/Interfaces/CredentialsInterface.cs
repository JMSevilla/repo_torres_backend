using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
namespace TORRES_backend.Interfaces
{
    public interface CredentialsInterface
    {
        
        void tokenInterface(string email, string token);
        void detroyInterface(string email);
        void checkTokenInterface(string token, string email);
        void sessionBlockerInterface(); //stage 1

        IHttpActionResult SessionWillUpdate(string email);
    }
}
