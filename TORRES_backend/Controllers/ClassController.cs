using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TORRES_backend.Helpers;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace TORRES_backend.Controllers
{
    [RoutePrefix("api/class")]
    public class ClassController : ApiController
    {
        classHelper __helper = new classHelper();
        // POST: api/employeeUsers
        [Route("add-class"), System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> PostemployeeUser()
        {

            try
            {
                IHttpActionResult result = null;
                __helper.classAdding();
                result = Ok(classHelper.response);
                return await Task.FromResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}