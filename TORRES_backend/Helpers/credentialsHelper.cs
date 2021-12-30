using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TORRES_backend.Interfaces;
using TORRES_backend.APIConnection;
using TORRES_backend._dataBind._bindHelper;
using static TORRES_backend._dataBind._bindHelper.signinBindHelper;
using TORRES_backend.Models;
using static TORRES_backend._dataBind._bindHelper.tokenBindHelper;

namespace TORRES_backend.Helpers
{
    public class credentialsHelper : ApiController, CredentialsInterface
    {


        public static dynamic getSigninResponse;
        public static Boolean doneLogout;
        
        public class BEWillResponse
        {
            public String status { get; set; }
            public Object databulk {get;set;}
        }
       
         class dbProcessor
        {
            public torresfullstackdbEntities __classDB { get; set; }
        }
        class Decryption{
            public string orig {get;set;}
            public string decryptRequest {get;set;}
        }
        class loginAppend
        {
            signinBindHelper bind = new signinBindHelper();
            initialState state = new initialState();
            APISecurity auth = new APISecurity();
            BEWillResponse ESignResponse = new BEWillResponse();
            Decryption desc = new Decryption();
            public void _login_shifts()
            {
                var server = HttpContext.Current.Request;
                
                using(Connection._publicDB)
                {
                    bind.email = server.Form["email"];
                    bind.password = auth.Encrypt(server.Form["password"]);
                    bind._encrypted = string.Empty;
                    state.setObject(bind);
                    var _self_1 = Connection._publicDB.adminusers.Any(x => x.email == state.signObj.email && x.is_verified != "0");
                    var _self_2 = Connection._publicDB.adminusers.Where(x => x.email == state.signObj.email).FirstOrDefault();
                    var _self_3 = Connection._publicDB.users.Any(x => x.email == state.signObj.email && x.is_archive != "1");
                    var _self_4 = Connection._publicDB.users.Where(x => x.email == state.signObj.email).FirstOrDefault();

                    Validate(state.signObj.email, state.signObj.password);

                    if(_self_1){
                        state.signObj._encrypted = _self_2 == null ? "" : _self_2.password;
                        state.signObj.istype = _self_2.is_type;
                        state.signObj.isstatus = _self_2.is_verified;
                        desc.orig = auth.Decrypt(state.signObj.password);
                        desc.decryptRequest = auth.Decrypt(state.signObj._encrypted);
                        if(desc.decryptRequest == desc.orig){
                            if(state.signObj.isstatus == "1"){
                                if(state.signObj.istype == "1"){
                                    var getdata = Connection._publicDB
                                    .adminusers.Where(x => x.email == state.signObj.email).Select(t => new {
                                        t.adminID,
                                        t.firstname,
                                        t.lastname,
                                        t.is_verified
                                    }).ToList();
                                    ESignResponse.databulk = getdata.FirstOrDefault();
                                    ESignResponse.status = "SUCCESS ADMIN";
                                    getSigninResponse = ESignResponse;
                                    return;
                                }else{
                                    ESignResponse.status = "no user";
                                    getSigninResponse = ESignResponse;
                                    return;
                                }
                            }else{
                                ESignResponse.status = "account disabled";
                                getSigninResponse = ESignResponse;
                                return;
                            }
                        }else{
                            ESignResponse.status = "wrong password";
                            getSigninResponse = ESignResponse;
                            return;
                        }
                    }
                }
            }
            void Validate(string email, string password)
            {
                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)){
                    ESignResponse.status = "empty";
                    getSigninResponse = ESignResponse;
                    return;
                }
            }
        }
        public void loginInterface()
        {
            loginAppend logs = new loginAppend();
            logs._login_shifts();
        }

        class tokenAppend
        {
            torresfullstackdbEntities core = Connection._publicDB;
            BEWillResponse ESignResponse = new BEWillResponse();
            public void _shift_Token(string email, string token)
            {
                try
                {
                    using (core)
                    {
                        var check = core.adminusers.Where(x => x.email == email).FirstOrDefault();
                        if(check != null)
                        {
                            check.istoken = token;
                            core.SaveChanges();
                            ESignResponse.status = "update token admin";
                            ESignResponse.databulk = core.adminusers
                                .Where(y => y.email == email).Select(t => new
                                {
                                    t.istoken
                                }).ToList();

                            getSigninResponse = ESignResponse;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            public void destroyToken(string email)
            {
                try
                {
                    using(core)
                    {
                        var check = core.adminusers.Where(x => x.email == email).FirstOrDefault();
                        if (check != null)
                        {
                            check.istoken = "";
                            core.SaveChanges();
                            doneLogout = true;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            public void checkToken(string token, string email)
            {
                try
                {
                    using (core)
                    {

                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            void tokenValidationWillCheck(string token, string email)
            {
                if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email)){
                    ESignResponse.status = "empty";
                }
            }
        }
        tokenAppend t = new tokenAppend();
        public void tokenInterface(string email, string token)
        {
            t._shift_Token(email, token);
        }

        public void tokenDestroyInterface(string email)
        {
            t.destroyToken(email);
        }

        public void scanTokenInterface(string token, string email)
        {
            throw new NotImplementedException();
        }
    }
}