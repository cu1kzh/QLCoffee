using QLCoffee.Areas.Admin.AdminService.DependencyInjectionNCC;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Areas.Admin.AdminService.ICommandNCC
{
    public class CreateNhaCungCapCommand : ICommand
    {
        private readonly IQuanLyQuanCoffeeEntities _database;
        private readonly NHACUNGCAP _nhacungcap;

        public CreateNhaCungCapCommand(IQuanLyQuanCoffeeEntities database, NHACUNGCAP nhacungcap)
        {
            _database = database;
            _nhacungcap = nhacungcap;
        }

        public void Execute()
        {
            _database.Add(_nhacungcap);
            _database.Save();
        }
    }
}