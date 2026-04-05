using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Areas.Admin.AdminService
{
    public class DatabaseSingleton
    {
        private static DatabaseSingleton _instance;
        private static readonly object _lock = new object();
        public QuanLyQuanCoffeeEntities Database { get; private set; }

        private DatabaseSingleton()
        {
            Database = new QuanLyQuanCoffeeEntities();
        }

        public static DatabaseSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseSingleton();
                        }
                    }
                }   
                return _instance;
            }
        }
    }
}