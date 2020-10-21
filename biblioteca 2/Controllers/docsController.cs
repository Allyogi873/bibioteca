using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using biblioteca_2.Models;
using biblioteca_2.ayuda;

namespace biblioteca_2.Controllers
{
    public class docsController : Controller
    {
        List<categoria> holi = new List<Models.categoria>()
        {
            new categoria() { id =0, nombre = "generalidad" },
            new categoria() { id =1, nombre = "Filosofia y Psicologia" },
            new categoria() { id =2, nombre = "Religion" },
            new categoria() { id =3, nombre = "Ciencias Sociales" },
            new categoria() { id =4, nombre = "Lenguajes" },
            new categoria() { id =5, nombre = "Ciencias Natuales y Matematicas" },
            new categoria() { id =6, nombre = "Tecnologia" },
            new categoria() { id =7, nombre = "Artes" },
            new categoria() { id =8, nombre = "Literatura" },
            new categoria() { id =9, nombre = "Geografia e Historia" },
            new categoria() { id =10, nombre = "Tesis (dame copia)" },
        };
        private Database2 db = new Database2();

        // GET: docs
        public ActionResult Index()
        {
            return View(db.docs.ToList());
        }

        // GET: docs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            docs docs = db.docs.Find(id);
            if (docs == null)
            {
                return HttpNotFound();
            }
            return View(docs);
        }

        // GET: docs/Create
        public ActionResult Create()
        {
            ViewBag.categoria = new SelectList(holi, "id", "nombre");
            return View();
        }

        // POST: docs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(docs docs)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        var file = Request.Files[0];
                        string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                        docs.dir_archivo = archivo;
                        file.SaveAs(Server.MapPath("~/subidas/archivos/" + archivo));
                    }
                    db.docs.Add(docs);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(docs);
            }
            catch (Exception e){
                return RedirectToAction("Create");
            }
        }

        // GET: docs/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.categoria = new SelectList(holi, "id", "nombre");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            docs docs = db.docs.Find(id);
            if (docs == null)
            {
                return HttpNotFound();
            }
            return View(docs);
        }

        // POST: docs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(docs docs)
        {
            docs original = db.docs.Find(docs.id_archivo);

            try
            {
                if (ModelState.IsValid)
                {
                    original.nombre = docs.nombre;
                    if (docs.categoria != null)
                    {
                        original.categoria = docs.categoria;
                    }
                    original.autor = docs.autor;
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        var file = Request.Files[0];
                        string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                        original.dir_archivo = archivo;
                        file.SaveAs(Server.MapPath("~/subidas/archivos/" + archivo));
                    }
                    db.Entry(original).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            
            return View(original);
        }
            catch (Exception e){
                return RedirectToAction("index");
    }
}

        // GET: docs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            docs docs = db.docs.Find(id);
            if (docs == null)
            {
                return HttpNotFound();
            }
            return View(docs);
        }

        // POST: docs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            docs docs = db.docs.Find(id);
            db.docs.Remove(docs);
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
