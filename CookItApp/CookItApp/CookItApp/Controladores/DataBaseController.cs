using CookItApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Controladores
{
    public class DataBaseController
    {

        public TokenDataBaseController Token { get; }        
        public UsuarioDataBaseController Usuario { get; }
        public PerfilDataBaseController Perfil { get; }
        public MomentoDiaDataBaseController MomentoDia { get; }
        public EstacionDataBaseController Estacion { get; }
        public RecetaDataBaseController Receta { get; }
        public IngredienteUsuarioDataBaseController IngredienteUsuario { get; }
        public IngredienteDataBaseController Ingrediente { get; }
        public IngredienteRecetaDataBaseController IngredienteReceta { get; }
        public RetoDataBaseController Reto { get; }
        public RecetaFavoritaDataBaseController RecetaFavorita { get; }
        public NotificacionDataBaseController Notificacion { get; }
        public ComentarioRecetaDataBaseController ComentarioReceta { get; }
        public PasoRecetaDataBaseController PasoReceta { get; }
        public HistorialRecetaDatabaseController HistorialReceta { get; }
        public EstadoRetoDataBaseController EstadoReto { get; }
        public SupermercadoDataBaseController Supermercado { get; }

        public DataBaseController()
        {
            Token = new TokenDataBaseController();
            Usuario = new UsuarioDataBaseController();
            Perfil = new PerfilDataBaseController();
            MomentoDia = new MomentoDiaDataBaseController();
            Estacion = new EstacionDataBaseController();
            Receta = new RecetaDataBaseController();
            IngredienteUsuario = new IngredienteUsuarioDataBaseController();
            Ingrediente = new IngredienteDataBaseController();
            IngredienteReceta = new IngredienteRecetaDataBaseController();
            Reto = new RetoDataBaseController();
            RecetaFavorita = new RecetaFavoritaDataBaseController();
            Notificacion = new NotificacionDataBaseController();
            ComentarioReceta = new ComentarioRecetaDataBaseController();
            PasoReceta = new PasoRecetaDataBaseController();
            HistorialReceta = new HistorialRecetaDatabaseController();
            EstadoReto = new EstadoRetoDataBaseController();
            Supermercado = new SupermercadoDataBaseController();
        }

        public void BorrarTodo()
        {
            Token.BorrarTodo();
            MomentoDia.BorrarTodo();
            Estacion.BorrarTodo();            
            IngredienteUsuario.BorrarTodo();
            Ingrediente.BorrarTodo();
            IngredienteReceta.BorrarTodo();
            Reto.BorrarTodo();
            RecetaFavorita.BorrarTodo();
            Notificacion.BorrarTodo();
            ComentarioReceta.BorrarTodo();
            PasoReceta.BorrarTodo();
            HistorialReceta.BorrarTodo();
            EstadoReto.BorrarTodo();
            Receta.BorrarTodo();
            Perfil.BorrarTodo();
            Usuario.BorrarTodo();
            Supermercado.BorrarTodo();

        }
    }
}
