using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TORRES_backend.Models;
namespace TORRES_backend.APIConnection
{
    public static class Connection
    {
        private static torresfullstackdbEntities _APIDB;
        public static torresfullstackdbEntities _publicDB
        {
            get
            {
                _APIDB = new torresfullstackdbEntities();
                return _APIDB;
            }
        }
    }
}