﻿using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class RecetaFavoritaDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public RecetaFavoritaDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<RecetaFavorita>();
        }

        public List<RecetaFavorita> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<RecetaFavorita>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<RecetaFavorita>().ToList();
                }
            }
        }

        public int GuardarList(List<RecetaFavorita> obj)
        {

            lock (locker)
            {

                if (obj.Count > 0)
                {
                    return database.InsertAll(obj);
                }
                else
                {
                    return 0;
                }

            }
        }

        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<RecetaFavorita>();
            }
        }
    }
}