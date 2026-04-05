using QLCoffee.Areas.Admin.AdminService.DependencyInjectionNCC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Areas.Admin.AdminService.ICommandNCC
{
    public class DeleteNhaCungCapCommand : ICommand
    {
        private readonly IQuanLyQuanCoffeeEntities _database;
        private readonly string _id;

        public DeleteNhaCungCapCommand(IQuanLyQuanCoffeeEntities database, string id)
        {
            _database = database;
            _id = id;
        }

        public void Execute()
        {
            var nhacungcap = _database.NHACUNGCAPs.FirstOrDefault(s => s.MaNCC == _id);
            if (nhacungcap != null)
            {
                _database.Delete(nhacungcap);
                _database.Save();
            }
        }
    }
}