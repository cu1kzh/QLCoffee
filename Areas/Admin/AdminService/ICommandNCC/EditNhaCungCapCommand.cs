using QLCoffee.Areas.Admin.AdminService.DependencyInjectionNCC;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Areas.Admin.AdminService.ICommandNCC
{
    public class EditNhaCungCapCommand : ICommand
    {
        private readonly IQuanLyQuanCoffeeEntities _database;
        private readonly NHACUNGCAP _nhacungcap;

        public EditNhaCungCapCommand(IQuanLyQuanCoffeeEntities database, NHACUNGCAP nhacungcap)
        {
            _database = database;
            _nhacungcap = nhacungcap;
        }

        public void Execute()
        {
            _database.Update(_nhacungcap);
            _database.Save();
        }
    }
}