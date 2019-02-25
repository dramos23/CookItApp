using CookItApp.Data;
using CookItApp.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{
    public class NuevaRecetaVM
    {
        public Usuario Usuario { get; set; }
        public Receta Receta { get; set; }
        public ImageSource Foto { get; set; }
        public string Email { get; set; }
        public DateTime Fecha { get; set; }

        public ObservableCollection<Estacion> ItemsEstacion { get; set; }
        public ObservableCollection<MomentoDia> ItemsMomentoDia { get; set; }
        public ObservableCollection<Dificultad> ItemsDificultad { get; set; }

        public NuevaRecetaVM(Usuario usuario)
        {
            
            Usuario = usuario;
            Foto = "noimage.png";
            Receta = new Receta();
            Cargar_Valores_Defecto_Receta();
            CargarPickerEstacion();
            CargarPickerMomentoDia();
            CargarPickerDificultad();


        }

        private void CargarPickerDificultad()
        {
            List<Dificultad> list = new List<Dificultad>()
            {
                new Dificultad(){ Key = "1", value = 1 },
                new Dificultad(){ Key = "2", value = 2 },
                new Dificultad(){ Key = "3", value = 3 },
                new Dificultad(){ Key = "4", value = 4 },
                new Dificultad(){ Key = "5", value = 5 },
            };

            ItemsDificultad = new ObservableCollection<Dificultad>(list);
        }

        internal async Task<int> SubirReceta()
        {
            int idReceta = await App.RecetaService.Alta(Receta);

            if (idReceta != -1)
            {
                Receta.AsignarId(idReceta);

                int guardado = App.DataBase.Receta.Guardar(Receta);

                if (guardado == 1)
                {
                    return 1;
                }
                return 2;
            }
            return 3;
        }

        internal void CargarDatosReceta(MediaFile foto, string titulo, string descripcion, int tiempo, int platos ,int dificultad, int momento, int estacion)
        {
            Receta._Foto = ImageToByteArray(foto);
            Receta._Titulo = titulo;
            Receta._Descripcion = descripcion;
            Receta._TiempoPreparacion = tiempo;
            Receta._Dificultad = dificultad;
            Receta._IdMomentoDia = momento;
            Receta._IdEstacion = estacion;
            Receta._CantPlatos = platos;

        }

        private void Cargar_Valores_Defecto_Receta()
        {
            Email = Usuario._Email;
            Fecha = DateTime.Now;
            
            Receta._Email = Email;
            Receta._FechaCreacion = Fecha;
            Receta._PuntajeTotal = 2.5;
            Receta._Habilitada = false;
        }

        private void CargarPickerEstacion()
        {
            List<Estacion> estaciones = App.DataBase.Estacion.ObtenerList();
            if (estaciones != null && estaciones.Count > 0)
            {
                ItemsEstacion = new ObservableCollection<Estacion>(estaciones);
            }
            else
            {
                ItemsEstacion = new ObservableCollection<Estacion>();
            }
        }

        private void CargarPickerMomentoDia()
        {
            List<MomentoDia> momentoDias = App.DataBase.MomentoDia.ObtenerList();
            if (momentoDias != null && momentoDias.Count > 0)
            {
                ItemsMomentoDia = new ObservableCollection<MomentoDia>(momentoDias);
            }
            else
            {
                ItemsMomentoDia = new ObservableCollection<MomentoDia>();
            }

        }

        internal void AgregarIngredientes(List<IngredienteReceta> ingredientes)
        {
            Receta._ListaIngredientesReceta = ingredientes;
        }

        internal void AgregarPasos(List<PasoReceta> pasos)
        {
            Receta._ListaPasosReceta = pasos;
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

    public class Dificultad
    {
        public string Key { get; set; }
        public int value { get; set; }
    }
}
