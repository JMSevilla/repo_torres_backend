//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TORRES_backend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        public int client_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public Nullable<int> age { get; set; }
        public string gender { get; set; }
        public string contactnum { get; set; }
        public string province { get; set; }
        public string municipality { get; set; }
        public string zip_code { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string image_url { get; set; }
        public string is_verified { get; set; }
        public string is_type { get; set; }
        public string is_activate { get; set; }
        public string is_token_verified { get; set; }
        public string is_google_verified { get; set; }
        public string is_archive { get; set; }
        public Nullable<System.DateTime> createdAt { get; set; }
    }
}
