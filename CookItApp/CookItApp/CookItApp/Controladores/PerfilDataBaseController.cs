using CookItApp.Models;
using SQLite;
using Xamarin.Forms;

namespace CookItApp.Data
{
    public class PerfilDataBaseController
    {

        static readonly object locker = new object();

        SQLiteConnection database;

        public PerfilDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<Perfil>();
            
        }

        public Perfil Obtener(string email)
        {
            lock (locker)
            {
                if (database.Table<Perfil>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Perfil>().Where(p => p._Email == email).First();
                }
            }
        }

        public int Modificar(Perfil perfil)
        {
            lock (locker)
            {
                if (database.Table<Perfil>().Count() != 0)
                {                    
                    return database.Update(perfil);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Guardar(Perfil perfil)
        {

            lock (locker)
            {

                Perfil p = Obtener(perfil._Email);

                if (p == null)
                {
                    return database.Insert(perfil);
                }
                else {
                    return 0;
                }                
            }
        }

        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<Perfil>();
            }
        }
    }
}
