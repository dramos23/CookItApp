using CookItApp.Models;
using SQLite;
using Xamarin.Forms;

namespace CookItApp.Data
{
    public class TokenDatabaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public TokenDatabaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<Token>();
        }

        public Token Obtener()
        {
            lock (locker)
            {
                if (database.Table<Token>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Token>().First();
                }
            }
        }

        public int Guardar(Token token)
        {
            lock (locker)
            {
                if (token._Id != 0)
                {
                    database.Update(token);
                    return token._Id;
                }
                else
                {
                    return database.Insert(token);
                }
            }
        }
        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<Token>();
            }
        }
    }
}
