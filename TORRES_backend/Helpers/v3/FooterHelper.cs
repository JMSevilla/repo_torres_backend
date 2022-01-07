using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TORRES_backend.APIConnection;
using TORRES_backend.Interfaces;
using TORRES_backend.Models;

namespace TORRES_backend.Helpers.v3
{
    public class FooterHelper : ApiController, IDynamicFooter
    {
        public static dynamic response;
        FooterFunctionality footer = new FooterFunctionality();
        class FooterFunctionality
        {
            dbtorresEntities core;
            public void shifts()
            {
                using (core = Connection._publiccloud)
                {
                    var obj = core.dynamicFooters.ToList();
                    response = obj;
                }
            }
        }
        public IHttpActionResult GetContent()
        {
            try
            {
                footer.shifts();
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}