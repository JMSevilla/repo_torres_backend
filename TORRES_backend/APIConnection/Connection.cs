using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TORRES_backend.Models;
namespace TORRES_backend.APIConnection
{
    public static class Connection
    {
        private static ttcdbEntities _APIDB;
        public static ttcdbEntities _publicDB
        {
            get
            {
                _APIDB = new ttcdbEntities();
                return _APIDB;
            }
        }
        private static dbttcEntities _clouddb;
        public static dbttcEntities _publiccloud
        {
            get
            {
                _clouddb = new dbttcEntities();
                return _clouddb;
            }
        }
    }
}