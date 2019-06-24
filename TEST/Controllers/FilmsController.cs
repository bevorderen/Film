using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TEST.Models;


namespace TEST.Controllers
{
    public class FilmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static readonly HashSet<String> AllowedExtensions = new HashSet<String> { ".jpg", ".jpeg", ".png", ".gif" };

        // GET: Films
        public ActionResult Index(int? currentPage)
        {
            if (currentPage != null)
                this.ViewBag.currentPage = currentPage;
            else
                this.ViewBag.currentPage = 0;
            return View(db.Films.ToList());
        }

        // GET: Films/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilmEditModel model)
        {
            var fileName = "";
            if (model.File != null)
            {
                fileName = System.IO.Path.GetFileName(model.File.FileName);
                var fileExt = Path.GetExtension(fileName).ToLower();
                if (!FilmsController.AllowedExtensions.Contains(fileExt))
                {
                    this.ModelState.AddModelError(nameof(model.File), "This file type is prohibited");
                }
                model.File.SaveAs(HostingEnvironment.MapPath("~/attachments/" + fileName));
            }

            if (ModelState.IsValid)
            {

                var Film = new Film
                {
                    Name = model.Name,
                    Description = model.Description,
                    Created = model.Created,
                    Regisseur = model.Regisseur,
                    CreatorName = User.Identity.Name,
                    Created_post = DateTime.Now
                };
                if (model.File != null)
                    Film.Path = "/attachments/" + fileName;

                db.Films.Add(Film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Films/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            var editFilm = new FilmEditModel
            {
                Name = film.Name,
                Description = film.Description,
                Created = film.Created,
                Regisseur = film.Regisseur,
                CreatorName = User.Identity.Name,
            };
            if (film == null)
            {
                return HttpNotFound();
            }
            this.ViewBag.Guid = film.Id;
            return View(editFilm);
        }

        // POST: Films/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, FilmEditModel model)
        {
            if (ModelState.IsValid)
            {
                
                var film = await this.db.Films.SingleOrDefaultAsync(m => m.Id == id);
                film.Name = model.Name;
                film.Regisseur = model.Regisseur;
                film.Description = model.Description;
                film.Created = model.Created;
                if (model.File != null)
                {
                    var fileName = System.IO.Path.GetFileName(model.File.FileName);
                    model.File.SaveAs(HostingEnvironment.MapPath("~/attachments/" + fileName));
                    film.Path = "/attachments/" + fileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
