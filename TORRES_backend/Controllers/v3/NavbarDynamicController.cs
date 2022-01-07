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
    [RoutePrefix("api/navbar-dynamic")]
    public class NavbarDynamicController : ApiController
    {
        NavbarHelper navFunction = new NavbarHelper();
        [Route("get-content"), HttpGet]
        public async Task<IHttpActionResult> getNavbarContent()
        {
            try
            {
                IHttpActionResult result = null;
                navFunction.getNavbarUI();
                result = Ok(NavbarHelper.response);
                return await Task.FromResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
