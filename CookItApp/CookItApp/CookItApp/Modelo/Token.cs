using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class Token
    {
        [PrimaryKey]
        public int _Id { get; set; }
        public string _AccessToken { get; set; }
        public DateTime _ExpireDate { get; set; }
        

        public Token()
        {

        }

        public Token(string AccessToken, DateTime ExpireDate)
        {
            _AccessToken = AccessToken;
            _ExpireDate = ExpireDate;
        }
    }
}
