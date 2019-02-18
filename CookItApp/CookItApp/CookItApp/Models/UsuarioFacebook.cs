using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class UsuarioFacebook
    {
        public string id { get; set; }

        public string email { get; set; }
        public string first_name { get; set; }        
        public string last_name { get; set; }

        public Picture picture { get; set; }

        public UsuarioFacebook() {}

        internal void ExisteEmail()
        {
            if (email == null)
            {
                email = id.ToString() + "@facebook.com";
            }
        }
    }

    public class Picture
    {
        public Data data { get; set; }

        public Picture() { }
    }

    public class Data
    {
        public string url { get; set; }

        public int height { get; set; }
        public bool is_silhouette { get; set; }
    
        public int width { get; set; }

        public Data() { }
    }
}
