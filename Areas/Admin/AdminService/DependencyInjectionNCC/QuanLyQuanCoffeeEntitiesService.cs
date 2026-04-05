using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Areas.Admin.AdminService.DependencyInjectionNCC
{
    public class QuanLyQuanCoffeeEntitiesService : IQuanLyQuanCoffeeEntities
    {
        private readonly QuanLyQuanCoffeeEntities _context;

        public QuanLyQuanCoffeeEntitiesService(QuanLyQuanCoffeeEntities context)
        {
            _context = context;
        }

        public IQueryable<NHACUNGCAP> NHACUNGCAPs => _context.NHACUNGCAPs;

        public void Add(NHACUNGCAP entity)
        {
            _context.NHACUNGCAPs.Add(entity);
        }

        public void Update(NHACUNGCAP entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(NHACUNGCAP entity)
        {
            _context.NHACUNGCAPs.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}