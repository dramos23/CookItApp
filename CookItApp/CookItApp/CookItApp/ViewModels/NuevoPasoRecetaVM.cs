using CookItApp.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{    


    public class NuevoPasoRecetaVM
    {
        public string Descripcion { get; set; }
        public int Tiempo { get; set; }
        public string URL { get; set; }
        public ImageSource Foto { get; set; }
        public PasoReceta PasoReceta { get; set; }
        private int IdPasoReceta { get; set; }
        

        public NuevoPasoRecetaVM(Models.PasoReceta pasoReceta, int idPasoReceta)
        {
            Foto = "noimage.png";
            CargarDatos(pasoReceta);
            IdPasoReceta = idPasoReceta;
        }

        public void CargarPasoRec(MediaFile foto, string descripcion, string uRL, int tiempo)
        {
            PasoReceta = new PasoReceta()
            {
                
                _IdPasoReceta = IdPasoReceta,
                _Descripcion = descripcion,
                _UrlVideo = uRL,
                _TiempoReloj = tiempo,
                _Foto = ImageToByteArray(foto)

            };
        }

        private void CargarDatos(PasoReceta pasoReceta)
        {
            if (pasoReceta != null)
            {
                PasoReceta = pasoReceta;

                Descripcion = pasoReceta._Descripcion;
                Tiempo = pasoReceta._TiempoReloj;
                URL = pasoReceta._UrlVideo;
                Foto = pasoReceta.GenerarImageSource();

            }
        }

        public byte[] ImageToByteArray(MediaFile FotoFile)
        {
            if (FotoFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    FotoFile.GetStream().CopyTo(memoryStream);
                    FotoFile.Dispose();
                    return memoryStream.ToArray();
                }
            }
            return null;
        }
    }
}
