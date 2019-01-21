using CookItApp.Models;
using SQLite;
using Xamarin.Forms;

namespace CookItApp.Data
{
    public class UsuarioDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public UsuarioDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<Usuario>();
        }

        public Usuario Obtener()
        {
            lock (locker)
            {
                if(database.Table<Usuario>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Usuario>().First();
                }
            }
        }


        public int Guardar(Usuario user)
        {
            lock (locker)
            {
                if(database.Table<Usuario>().Count() == 1)
                {
                    database.Update(user);
                    return 1;
                }
                else
                {
                    return database.Insert(user);
                }
            }
        }

        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<Usuario>();
            }
        }

    }
}
