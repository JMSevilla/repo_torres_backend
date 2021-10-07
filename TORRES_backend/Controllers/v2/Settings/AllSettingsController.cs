using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TORRES_backend.Models;
using TORRES_backend.APIConnection;
using System.Threading.Tasks;
using System.Web;

namespace TORRES_backend.Controllers.v2.Settings
{
    /* Non API 1.0 */
    [RoutePrefix("api/settings")]
    public class AllSettingsController : ApiController
    {
        dbttcEntities core = Connection._publiccloud;
        [Route("add-api-key/{apikey}/key"), HttpPost]
        public IHttpActionResult setApi(string apikey)
        {
            try
            {
                using (core)
                {
                    apikeyStorage key = new apikeyStorage();
                    key.apiIndicator = Guid.NewGuid().ToString();
                    key.apiKey = apikey;
                    key.createdAt = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM"));
                    key.isused = "0";
                    core.apikeyStorages.Add(key);
                    core.SaveChanges();
                    return Ok("success apikey");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("fetch-all-apilist"), HttpGet]
        public async Task<IHttpActionResult> listallapi()
        {
            try
            {
                IHttpActionResult res = null;
                using (core)
                {
                    var obj = core.apikeyStorages.ToList().Distinct();
                    res = Ok(obj);
                    return await Task.FromResult(res);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("fetch-key-list-platform"), HttpGet]
        public async Task<IHttpActionResult> getkeys()
        {
            try
            {
                IHttpActionResult result = null;
                using (core)
                {
                    var obj = core.views_apikey_list.ToList();
                    result = Ok(obj);
                    return await Task.FromResult(result);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        class PlatformData
        {
            public string response { get; set; }
            public string title { get; set; }
            public string alias { get; set; }
            public char access { get; set; }
            public DateTime createdAt { get; set; }
            public string apiKey { get; set; }
            public string img { get; set; }
        }
        PlatformData platform = new PlatformData();
        [Route("add-platform"), HttpPost]
        public IHttpActionResult addplat()
        {
            try
            {
                var HTTP = HttpContext.Current.Request;
                platform.title = HTTP.Form["title"];
                platform.alias = HTTP.Form["alias"];
                platform.access = Convert.ToChar(HTTP.Form["access"]);
                platform.apiKey = HTTP.Form["apikey"];
                platform.img = HTTP.Form["imageurl"];
                platform.createdAt = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM"));
                if(string.IsNullOrEmpty(platform.title) || string.IsNullOrEmpty(platform.alias)
                    || string.IsNullOrEmpty(platform.apiKey))
                {
                    return Ok(platform.response = "empty");
                }
                else
                {
                    using (core)
                    {
                        tb_Platform plat = new tb_Platform();
                        plat.title = platform.title;
                        plat.alias = platform.alias;
                        plat.access = Convert.ToString(platform.access);
                        plat.apiKeys = platform.apiKey;
                        plat.platformImage = platform.img;
                        plat.createdAt = platform.createdAt;
                        core.tb_Platform.Add(plat);
                        core.SaveChanges();
                        return Ok(platform.response = "success add platform");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
