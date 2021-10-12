using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TORRES_backend.Interfaces;
using TORRES_backend.Models;
using TORRES_backend.APIConnection;

namespace TORRES_backend.Helpers
{
    public class adminRegisterHelper : ApiController, AdminInterface
    {
        public static dynamic getDynamicAdmin;
        regClass g = new regClass();
        class regClass
        {
            public void _shift_checkAdmin()
            {
                using (Connection._Public_APIEntry)
                {
                    var checker = Connection._Public_APIEntry.adminusers.Any(x => x.is_type == "1" && x.is_verified == "1");
                    if (checker)
                    {
                        getDynamicAdmin = "exist";
                    }
                    else
                    {
                        getDynamicAdmin = "not exist";
                    }
                }
            }
        }
        public void detectAdminInterface()
        {
            g._shift_checkAdmin();
        }
    }
}