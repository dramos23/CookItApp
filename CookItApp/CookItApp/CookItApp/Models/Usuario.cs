﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace CookItApp.Models
{
    public class Usuario
    {
        public enum Tipo
        {
            Local = 1,
            Google = 2,
            Facebook = 3
        }

        
        public string _Email { get; set; }
        public System.Guid? _DeviceId { get; set; }
        public Tipo _Tipo { get; set; }
        [Ignore]        
        public string _Password { get; set; }
        public DateTime _UltimoIngreso { get; set; }
        [Ignore]
        [JsonIgnore]
        public Perfil _Perfil { set; get; }
        [Ignore]
        [JsonIgnore]
        public List<HistorialReceta> _ListaHistorialRecetas { get; set; }

        public Usuario() {

            

        }

        public Usuario(string Email, string Password, Guid? DeviceId, DateTime UltimoIngreso)
        {
            _Email = Email;
            _DeviceId = DeviceId;
            _Password = Password;
            _UltimoIngreso = UltimoIngreso;
        }

        public Usuario(string Email, string Password, Guid? DeviceId, Tipo Tipo, DateTime UltimoIngreso)
        {
            _Email = Email;
            _DeviceId = DeviceId;
            _Tipo = Tipo;
            _Password = Password;
            _UltimoIngreso = UltimoIngreso;
        }

        public bool IsValid()
        {
            return !_Email.Equals("") && !_Password.Equals("") ? true : false;
        }

        


    }    
}