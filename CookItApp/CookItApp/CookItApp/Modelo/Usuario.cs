using Newtonsoft.Json;
using SQLite;

namespace CookItApp.Models
{
    public class Usuario
    {
        [PrimaryKey]
        public string _Email { set; get; }
        public string _Password { set; get; }
        [Ignore]
        //[JsonIgnore]
        public Perfil _Perfil { set; get; }

        public Usuario() { }

        public Usuario(string Email, string _Password)
        {
            this._Email = Email;
            this._Password = _Password;
            this._Perfil = null;
            
        }

        public bool IsValid()
        {
            if (!this._Email.Equals("") && !this._Password.Equals(""))
                return true;
            else
                return false;
        }

        


    }    
}
