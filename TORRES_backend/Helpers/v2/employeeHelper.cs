using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TORRES_backend.Interfaces;
using TORRES_backend.APIConnection;
using TORRES_backend._dataBind._bindHelper;
using static TORRES_backend._dataBind._bindHelper.employeeBind;
using TORRES_backend.Models;

namespace TORRES_backend.Helpers.v2
{
    public class employeeHelper : ApiController, employeeInterface
    {
        public static string response;
        public static dynamic entityHelper;
        empDataWillMount data = new empDataWillMount();
        class empDataWillMount
        {
            APISecurity apis = new APISecurity();
            employeeBind state = new employeeBind();
            employeeState getState = new employeeState();
            employeeUser entityemp = new employeeUser();
            public void _empShift()
            {
                var HTTP = HttpContext.Current.Request;
                state.empname = HTTP.Form["empfullname"];
                state.empemail = HTTP.Form["empemail"];
                state.emppassword = apis.Encrypt(HTTP.Form["emppassword"]);
                state.apiaccess = Convert.ToChar(HTTP.Form["empapiaccesskey"]);
                state.apikey = apis.Encrypt(HTTP.Form["apiaccesskey"]);
                state.grantAccess = HTTP.Form["grantAccess"];
                state.createdAt = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd"));
                getState.getState(state);
                _empTurnover();
            }
            public void _empTurnover()
            {
                _empWillValidate
                    (
                    getState.empBind.empemail,
                    getState.empBind.emppassword,
                    Convert.ToString(getState.empBind.apiaccess),
                    getState.empBind.apikey,
                    getState.empBind.grantAccess
                    );
                entityemp.empName = getState.empBind.empname;
                entityemp.empEmail = getState.empBind.empemail;
                entityemp.empPassword = getState.empBind.emppassword;
                entityemp.APIAccess = Convert.ToString(getState.empBind.apiaccess);
                entityemp.APIKey = getState.empBind.apikey;
                entityemp.grantAccess = getState.empBind.grantAccess;
                entityemp.createdAt = getState.empBind.createdAt;
                entityHelper = entityemp;
            }
            public void _empWillValidate(
                    string email, string password,
                    string apiaccesskey, string api,
                    string grant
                )
            {
                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)
                    || string.IsNullOrEmpty(apiaccesskey) || string.IsNullOrEmpty(api)
                    || string.IsNullOrEmpty(grant))
                {
                    response = "empty";
                }
            }
        }
        public IHttpActionResult employeeAdding()
        {
            try
            {
                using (Connection._publiccloud)
                {
                    data._empShift();
                    Connection._publiccloud.employeeUsers.Add(entityHelper);
                    Connection._publiccloud.SaveChanges();
                    response = "success add employee";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}