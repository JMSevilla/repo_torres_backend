using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TORRES_backend.Helpers.v3;
using TORRES_backend.Models;
using TORRES_backend.APIConnection;
namespace TORRES_backend.Controllers.v3
{
    [RoutePrefix("api/v3/navbar-dynamic")]
    public class NavbarDynamicController : ApiController
    {
        NavbarHelper navFunction = new NavbarHelper();
        private dbtorresEntities core;
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
        [Route("get-content/{navbarIdentifier}"), HttpGet]
        public IHttpActionResult getNavbarIdentifier(int navbarIdentifier)
        {
            try
            {
                using(core = Connection._publiccloud)
                {
                    var obj = core.navbar_identifier.Where(x => x.navbarIdentifier
                    == navbarIdentifier).ToList();
                    return Ok(obj);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
