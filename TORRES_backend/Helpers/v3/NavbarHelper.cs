using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TORRES_backend.Interfaces.v3;
using TORRES_backend.APIConnection;
using TORRES_backend.Models;

namespace TORRES_backend.Helpers.v3
{
    public class NavbarHelper : ApiController, navbarInterface
    {
        public static dynamic response;
        NavigationBarLayer nav = new NavigationBarLayer();
        class NavigationBarLayer
        {
            public void getNavbar()
            {
                dbtorresEntities core;
                using(core = Connection._publiccloud)
                {
                    var obj = core.navbar_identifier_UI.ToList();
                    response = obj;
                }
            }
        }
        public IHttpActionResult getNavbarUI()
        {
            try
            {
                nav.getNavbar();
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}