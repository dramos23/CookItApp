using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookItApp.ViewModels
{
    public class NuevoPasosRecetaVM
    {

        public List<PasoReceta> PasosReceta { get; set; }

        public bool Vacio { get; set; }

        public bool Lista { get; set; }

        public string Text { get; set; }

        private int UltimoID { get; set; }

        public NuevoPasosRecetaVM(List<PasoReceta> pasosRecetas)
        {

            Vacio = false;
            Lista = true;
            UltimoID = 0;
            CargarIngredientesReceta(pasosRecetas);

        }

        private void CargarIngredientesReceta(List<PasoReceta> pasosRecetas)
        {
            if (pasosRecetas != null && pasosRecetas.Count > 0)
            {

                PasosReceta  = new List<PasoReceta>(pasosRecetas);

                UltimoID = PasosReceta.Select(x => x._IdPasoReceta).Max();

            }
            else
            {
                PasosReceta = new List<PasoReceta>();

                Lista = false;
                Vacio = true;
                Text = "Vamos ya casí que terminas faltan añadir los pasos de la receta!.";
            }
        }

        public void AgregarPasoRec(PasoReceta pasoReceta)
        {
            PasosReceta.Add(pasoReceta);
            UltimoID = pasoReceta._IdPasoReceta;

            Lista = true;
            Vacio = false;
        }

        public void ActualizarPasoRec(PasoReceta pasoReceta)
        {

            foreach (PasoReceta p in PasosReceta)
            {
                if (p._IdPasoReceta == pasoReceta._IdPasoReceta)
                {
                    p._Descripcion = pasoReceta._Descripcion;
                    p._Foto = pasoReceta._Foto;
                    p._TiempoReloj = pasoReceta._TiempoReloj;
                    p._UrlVideo = pasoReceta._UrlVideo;
                }
            }

        }

        public void EliminarPasoRec(PasoReceta pasoReceta)
        {
            int index = PasosReceta.FindIndex(i => i._IdPasoReceta == pasoReceta._IdPasoReceta);
            PasosReceta.RemoveAt(index);

            if (PasosReceta.Count == 0)
            {
                Lista = false;
                Vacio = true;
            }

        }

        public int Proximo_PasoRecetaID()
        {
            return UltimoID + 1;
        }
    }
}
