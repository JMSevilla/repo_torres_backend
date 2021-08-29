using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TORRES_backend.Models
{
    public class StudentClass
    {
        public string classcode { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime birthdate { get; set; }
        public int age { get; set; }
        public char gender { get; set; }
        public string contactnum { get; set; }
        public string province { get; set; }
        public string municipality { get; set; }
        public string zip_code { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string image_url { get; set; }
        public char is_verified { get; set; }
        public char is_type { get; set; }
        public char is_activate { get; set; }
        public char is_token_verified { get; set; }
        public char is_google_verified { get; set; }
        public char is_archive { get; set; }
        public DateTime createdAt { get; set; }
    }

    class Response
    {
        public string message { get; set; }
    }
}