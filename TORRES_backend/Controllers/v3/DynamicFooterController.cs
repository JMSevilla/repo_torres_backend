using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TORRES_backend.Helpers.v3;

namespace TORRES_backend.Controllers.v3
{
    [RoutePrefix("api/footer-get-content")]
    public class DynamicFooterController : ApiController
    {
        FooterHelper footerContent = new FooterHelper();
        [Route("get-content"), HttpPost]
         public async Task<IHttpActionResult> getFooter()
        {
            try
            {
                IHttpActionResult result;
                footerContent.GetContent();
                result = Ok(FooterHelper.response);
                return await Task.FromResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
