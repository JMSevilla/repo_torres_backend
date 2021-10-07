using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TORRES_backend.Interfaces;
using TORRES_backend.APIConnection;
using TORRES_backend._dataBind._bindHelper;
using static TORRES_backend._dataBind._bindHelper.StudentBind;
using TORRES_backend.Models;

namespace TORRES_backend.Helpers
{
    public class studentHelper : ApiController, StudentInterface
    {
        public static string studResp;
        public static dynamic entityGet;
        dataWillMount mount = new dataWillMount();
        class dataWillMount {
            StudentBind state = new StudentBind();
            studentState getState = new studentState();
            APISecurity apis = new APISecurity();
            user usr = new user();
            public void __studentShift()
            {
                var server = HttpContext.Current.Request;
                state.firstname = server.Form["firstname"];
                state.lastname = server.Form["lastname"];
                state.birthdate = Convert.ToDateTime(server.Form["birthdate"]);
                state.age = Convert.ToInt32(server.Form["age"]);
                state.gender = Convert.ToChar(server.Form["gender"]);
                state.contactnum = server.Form["contactnum"];
                state.province = server.Form["province"];
                state.municipality = server.Form["municipality"];
                state.zip_code = server.Form["zip_code"];
                state.address = server.Form["address"];
                state.email = server.Form["email"];
                state.password = apis.Encrypt(server.Form["password"]);
                state.image_url = server.Form["image_url"];
                state.is_type = Convert.ToChar("3");
                state.is_activate = Convert.ToChar("1");
                state.is_token_verified = Convert.ToChar("0");
                state.is_archive = Convert.ToChar("0");
                getState.__stateHandler(state);
                __entityShift();
            }
            public void __entityShift()
            {
                __studentBasicInfoWillValidate
                    (
                    getState.__bind.firstname,
                    getState.__bind.lastname,
                    getState.__bind.email,
                    getState.__bind.password
                    );
                usr.firstname = getState.__bind.firstname;
                usr.lastname = getState.__bind.lastname;
                usr.birthdate = getState.__bind.birthdate;
                usr.age = getState.__bind.age;
                usr.gender = Convert.ToString(getState.__bind.gender);
                usr.contactnum = getState.__bind.contactnum;
                usr.province = getState.__bind.province;
                usr.municipality = getState.__bind.municipality;
                usr.zip_code = getState.__bind.zip_code;
                usr.address = getState.__bind.address;
                usr.email = getState.__bind.email;
                usr.password = getState.__bind.password;
                usr.image_url = getState.__bind.image_url;
                usr.is_type = Convert.ToString(getState.__bind.is_type);
                usr.is_activate = Convert.ToString(getState.__bind.is_activate);
                usr.is_token_verified = Convert.ToString(getState.__bind.is_token_verified);
                usr.is_archive = Convert.ToString(getState.__bind.is_archive);
                usr.createdAt = getState.__bind.createdAt;
                usr.istoken = "";
                entityGet = usr;
            }
            public void __studentBasicInfoWillValidate(string firstname, string lastname,
                string email, string password)
            {
                if(string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname)
                    || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    studResp = "Required fields";
                }
            }
        }

        public IHttpActionResult addStudent()
        {
            try
            {
                using (Connection._publiccloud)
                {
                    Connection._publiccloud.users.Add(entityGet);
                    Connection._publiccloud.SaveChanges();
                    studResp = "success";
                    return Ok(studResp);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}