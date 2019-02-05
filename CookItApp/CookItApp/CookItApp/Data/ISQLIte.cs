using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp
{
    public interface ISQLIte
    {
        SQLiteConnection GetConnection();
        
    }
}
