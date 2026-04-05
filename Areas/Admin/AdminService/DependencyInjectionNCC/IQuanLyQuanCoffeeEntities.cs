using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCoffee.Areas.Admin.AdminService.DependencyInjectionNCC
{
    public interface IQuanLyQuanCoffeeEntities
    {
        IQueryable<NHACUNGCAP> NHACUNGCAPs { get; }
        void Add(NHACUNGCAP entity);
        void Update(NHACUNGCAP entity);
        void Delete(NHACUNGCAP entity);
        void Save();
    }
}
