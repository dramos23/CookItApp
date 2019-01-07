using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class UserInfoListVM : BaseViewModel
    {
        public ObservableCollection<Usuario> Usuarios { get; set; }

        public UserInfoListVM()
        {
            //Se filtra y se pone las recetas que queres mostrar

            //this.Usuarios = new ObservableCollection<UserInfo>();
            ////Just for tesing
            //this.Usuarios.Add(new UserInfo
            //{
            //    _Email = "dcazesv@gmail.com",
            //    _Id = 1,
            //    _Password = "12345"                
            //});
            //this.Usuarios.Add(new UserInfo
            //{
            //    _Email = "daniel.r.23@gmail.com",
            //    _Id = 2,
            //    _Password = "54321"
            //});
        }


    }
}
