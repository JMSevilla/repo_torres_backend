using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TORRES_backend._dataBind._bindHelper;
using TORRES_backend.APIConnection;
using TORRES_backend.Interfaces;
using TORRES_backend.Models;
using static TORRES_backend._dataBind._bindHelper.classBind;

namespace TORRES_backend.Helpers
{
    public class classHelper : ApiController, ClassInterface
    {
        private ttcdbEntities db = Connection._publicDB;
        public static string response;
        public static dynamic entityHelper;
        classDataWillMount data = new classDataWillMount();

        class classDataWillMount
        {
            classBind state = new classBind();
            classState getState = new classState();
            TbClass _class = new TbClass();

            public void _classShift()
            {
                var HTTP = HttpContext.Current.Request;
                state.classname = HTTP.Form["classname"];
                state.trainingUnder = HTTP.Form["trainingUnder"];
                state.status = HTTP.Form["status"];
                state.description = HTTP.Form["description"];
                state.createdAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                getState.getState(state);
                _classTurnOver();
            }
            public void _classTurnOver()
            {
                _class.classname = getState.classBind.classname;
                _class.trainingUnder = getState.classBind.trainingUnder;
                _class.status = getState.classBind.status;
                _class.description = Convert.ToString(getState.classBind.description);
                _class.createdAt = getState.classBind.createdAt;
                validateClassBasicInfo(_class);
                entityHelper = _class;
            }
            public void validateClassBasicInfo(TbClass _class)
            {
                if (string.IsNullOrEmpty(_class.classname) || string.IsNullOrEmpty(_class.trainingUnder)
                    || string.IsNullOrEmpty(_class.status) || string.IsNullOrEmpty(_class.description))
                {
                    response = "Missing fields!";
                }
            }


        }

        public IHttpActionResult classAdding()
        {
            try
            {
                using (db)
                {
                    data._classShift();
                    db.TbClasses.Add(entityHelper);
                    db.SaveChanges();
                    response = "success add employee";
                    return Ok(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}