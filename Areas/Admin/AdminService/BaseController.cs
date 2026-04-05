
using System.Web.Mvc;
using QLCoffee.Models;
using System.Linq;

namespace QLCoffee.Areas.Admin.Controllers.Base
{
    public abstract class BaseController<TEntity, TKey> : Controller where TEntity : class
    {
        protected QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();

        // Các phương thức trừu tượng mà lớp con phải triển khai
        protected abstract IQueryable<TEntity> GetAll();
        protected abstract TEntity GetById(TKey id);
        protected abstract void Add(TEntity entity);
        protected abstract void Update(TEntity entity);
        protected abstract void Remove(TEntity entity);
        protected abstract bool EntityExists(TKey id);

        // Template Method: Index action
        public virtual ActionResult Index()
        {
            return View(GetAll().ToList());
        }

        // Template Method: Create action - GET
        public virtual ActionResult Create()
        {
            return View();
        }

        // Template Method: Create action - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                Add(entity);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        // Template Method: Details action
        public virtual ActionResult Details(TKey id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            TEntity entity = GetById(id);

            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        // Template Method: Edit action - GET
        public virtual ActionResult Edit(TKey id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            TEntity entity = GetById(id);

            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        // Template Method: Edit action - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TKey id, TEntity entity)
        {
            if (ModelState.IsValid)
            {
                Update(entity);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        // Template Method: Delete action - GET
        public virtual ActionResult Delete(TKey id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            TEntity entity = GetById(id);

            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        // Template Method: Delete action - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(TKey id)
        {
            TEntity entity = GetById(id);
            Remove(entity);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}