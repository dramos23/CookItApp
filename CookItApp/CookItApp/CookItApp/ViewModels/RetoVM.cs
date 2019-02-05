using CookItApp.Data;
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
        
        public bool EtapaDos { get; set; }
        public bool EtapaTres { get; set; }
        public bool EtapaCuatro { get; set; }
        public bool Finalizado { get; set; }

        public string Acepto { get; set; }
        public string Cancelo { get; set; }
        public string DescDesafio { get; set; }
        public string Preg { get; set; }

        

        public RetoVM(Usuario usuario, Reto reto) {
            
            EtapaDos = false;
            EtapaTres = false;
            EtapaCuatro = false;
            Finalizado = false;            

            Reto = reto;            
            ConfEtapaUno(usuario);
        }


        private void ConfEtapaUno(Usuario usuario)
        {
            switch (Reto._IdEstadoReto)
            {
                case 1:
                    if (usuario._Email == Reto._EmailUsuDes)
                    {
                        EtapaDos = true;
                        Acepto = "Aceptó el desafío!";
                        Cancelo = "Paso!";
                        Preg = "Desea aceptar el desafío?";
                        DescDesafio = "Si aceptas el desafío deberas preparar la receta indicada, " +
                            "al finalizar deberas sacarte una foto con la presentación del plato y " +
                            "subirla en la aplicacion para que tu retador evalue lo que has " +
                            "preparado.";
                        DescDesafio = (DescDesafio as string).Replace("\\n", Environment.NewLine + Environment.NewLine);
                    }
                    break;
                case 2:
                    if (usuario._Email == Reto._EmailUsuDes)
                    {
                        EtapaTres = true;
                    }
                    break;
                case 4:
                    if (usuario._Email == Reto._EmailUsuOri)
                    {
                        EtapaCuatro = true;
                    }
                    if (usuario._Email == Reto._EmailUsuDes)
                    {
                        Finalizado = true;
                    }
                    break;
                case 5:
                    Finalizado = true;
                    break;
                case 6:
                    Finalizado = true;
                    break;


            }
            
        }
    }
}
