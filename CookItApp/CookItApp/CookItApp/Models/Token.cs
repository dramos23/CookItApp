using System;

namespace CookItApp.Models
{
    public class Token
    {
        
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
