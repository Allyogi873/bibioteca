using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using biblioteca_2.Models;
using PagedList;
using PagedList.Mvc;
using biblioteca_2.ayuda;





namespace biblioteca_2.Controllers
{
    public class EntradasController : Controller
    {
        private Database2 db = new Database2();

        // GET: Entradas
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var entradas = (from m in db.entrada
                            orderby m.fecha_entrada descending
                            select m).ToList();

            var unaPagina = entradas.ToPagedList(pageNumber, 5);
            ViewBag.pagina = unaPagina;
            var entrada = db.entrada.Include(e => e.AspNetUsers).Include(e => e.docs);
            return View(entrada.ToList());
        }
        // GET: Entradas Por Usuario
        public ActionResult Index2(int? page2)
        {
            var usuario = ayudas.ExtraerUs();
            var pageNumber2 = page2 ?? 1;
            var entradas2 = (from n in db.entrada
                            where n.UserId.Contains(usuario)
                            orderby n.fecha_entrada descending
                            select n).ToList();

            var unaPagina2 = entradas2.ToPagedList(pageNumber2, 5);
            ViewBag.pagina2 = unaPagina2;
            var entrada = db.entrada.Include(e => e.AspNetUsers).Include(e => e.docs);
            return View(entrada.ToList());
        }

        // GET: Entradas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entrada entrada = db.entrada.Find(id);
            if (entrada == null)
            {
                return HttpNotFound();
            }
            return View(entrada);
        }
        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entrada entrada = db.entrada.Find(id);
            if (entrada == null)
            {
                return HttpNotFound();
            }
            return View(entrada);
        }

        // GET: Entradas/Create
        public ActionResult Create()
        {
            string aux = ayudas.ExtraerUs();
            ViewBag.usuario = aux;
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.id_archivo = new SelectList(db.docs, "id_archivo", "nombre");
            return View();
        }

        // POST: Entradas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(entrada entrada)
        {

            if (ModelState.IsValid)
            {
                entrada.fecha_entrada = DateTime.Now;
                if (entrada.fecha_publicacion <= DateTime.Now.Date)
                {
                    entrada.estado = "1";
                }
                else
                {
                    entrada.estado = "2";
                }
                if (entrada.id_archivo == null) {
                    entrada.id_archivo = 10;
                }
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                    entrada.portada = archivo;
                    file.SaveAs(Server.MapPath("~/subidas/portadas/" + archivo));
                }
                db.entrada.Add(entrada);
                db.SaveChanges();
                return RedirectToAction("Index2");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", entrada.UserId);
            ViewBag.id_archivo = new SelectList(db.docs, "id_archivo", "nombre", entrada.id_archivo);

            return View(entrada);
        }

        public JsonResult getLibro(String busqueda)
        {
            var query = from m in db.docs
                        where m.nombre.Contains(busqueda) || m.autor.Contains(busqueda) 
                        select new { m.id_archivo, m.nombre, m.dir_archivo, m.categoria, m.autor };


            return Json(query.ToList(), JsonRequestBehavior.AllowGet);
        }






        // GET: Entradas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entrada entrada = db.entrada.Find(id);
            if (entrada == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", entrada.UserId);
            ViewBag.id_archivo = new SelectList(db.docs, "id_archivo", "nombre", entrada.id_archivo);
            return View(entrada);
        }

        // POST: Entradas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(entrada entrada)
        {
            entrada original = db.entrada.Find(entrada.cod_entrada);

            if (ModelState.IsValid)
            {
                original.titulo = entrada.titulo;
                original.contenido = entrada.contenido;
                original.meta = entrada.meta;
                if (entrada.id_archivo != null) 
                {
                    original.id_archivo = entrada.id_archivo;
                }
                if (entrada.fecha_publicacion != null)
                {
                    original.fecha_publicacion = entrada.fecha_publicacion;
                    if (entrada.fecha_publicacion <= DateTime.Now.Date)
                    {
                        original.estado = "1";
                    }
                    else
                    {
                        original.estado = "2";
                    }
                }
                
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                    original.portada = archivo;  
                    file.SaveAs(Server.MapPath("~/subidas/portadas/" + archivo));
                }


                db.Entry(original).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", entrada.UserId);
            ViewBag.id_archivo = new SelectList(db.docs, "id_archivo", "nombre", entrada.id_archivo);
            return View(original);
        }

        // GET: Entradas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entrada entrada = db.entrada.Find(id);
            if (entrada == null)
            {
                return HttpNotFound();
            }
            return View(entrada);
        }

        // POST: Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            entrada entrada = db.entrada.Find(id);
            db.entrada.Remove(entrada);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }



        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entrada entrada = db.entrada.Find(id);
            if (entrada == null)
            {
                return HttpNotFound();
            }
            return View(entrada);
        }

        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            entrada entrada = db.entrada.Find(id);
            db.entrada.Remove(entrada);
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
