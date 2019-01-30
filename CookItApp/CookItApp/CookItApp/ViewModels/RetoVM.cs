using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CookItApp.ViewModels
{
    public class RetoVM
    {

        public Reto Reto { get; set; }
        public bool EtapaUno { get; set; }

        public RetoVM(Usuario usuario, Reto reto) {

            Reto = reto;

            ConfEtapaUno(usuario);
        }

        private void ConfEtapaUno(Usuario usuario)
        {
            if (Reto._EmailUsuOri == usuario._Email && Reto._EstadoReto._IdEstadoReto == Reto._IdEstadoReto)
            {
                EtapaUno = true;
            }
            else {
                EtapaUno = false;
            }
        }
    }
}
