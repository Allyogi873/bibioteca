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
    public class comentariosController : Controller
    {
        private Database2 db = new Database2();
        public int aux; public int? aux2;
 
        // GET: comentarios
        public ActionResult Index()
        {
            var comentario = db.comentario.Include(c => c.AspNetUsers1);
            return View(comentario.ToList());
        }

        // GET: comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentario comentario = db.comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: comentarios/Create
        [ValidateInput(false)]
        public ActionResult Create(int id, string titu, string usu, string corre, string direccion)
        {
            this.aux = id;
            //titua = titu; correa = corre; porta = port; fecha1a = fecha1; conta = cont; fecha2a = fecha2; meta = met;
            ViewBag.correo = corre;
            ViewBag.usu = usu;
            ViewBag.dire = direccion;
            
                string aux = ayudas.ExtraerUs();
                ViewBag.usuario = aux;
            
            foreach (var item in db.entrada)
            {
                if (item.cod_entrada == id)
                {
                    ViewBag.id = item.cod_entrada;
                    ViewBag.id2 = item.cod_entrada;
                    ViewBag.titulo = item.titulo;
                    ViewBag.portada = item.portada;
                    ViewBag.fech1 = item.fecha_publicacion;
                    ViewBag.fech2 = item.fecha_entrada;
                    ViewBag.metas = item.meta;
                }
            }
            
            ViewBag.id_entrada = new SelectList(db.entrada, "cod_entrada", "titulo");
            ViewBag.UserId_com = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(comentario comentario)
        {
            if (ModelState.IsValid)
            {

                aux2 = comentario.id_entrada;
                comentario.fecha_comentario = DateTime.Now;

                db.comentario.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("../blogs/Details/" + aux2);
            }

            ViewBag.id_entrada = new SelectList(db.entrada, "cod_entrada", "titulo", comentario.id_entrada);
            ViewBag.UserId_com = new SelectList(db.AspNetUsers, "Id", "Email", comentario.UserId_com);
            return View(comentario);
        }

        // GET: comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentario comentario = db.comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_entrada = new SelectList(db.entrada, "cod_entrada", "titulo", comentario.id_entrada);
            ViewBag.UserId_com = new SelectList(db.AspNetUsers, "Id", "Email", comentario.UserId_com);
            return View(comentario);
        }

        // POST: comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_comentario,id_entrada,UserId_com,cont_comentario,fecha_comentario")] comentario comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.fecha_comentario = DateTime.Now;
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                aux2 = comentario.id_entrada;
                return RedirectToAction("../blogs/Details/" + aux2);
            }
            ViewBag.id_entrada = new SelectList(db.entrada, "cod_entrada", "titulo", comentario.id_entrada);
            ViewBag.UserId_com = new SelectList(db.AspNetUsers, "Id", "Email", comentario.UserId_com);
            return View(comentario);
        }

        // GET: comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentario comentario = db.comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comentario comentario = db.comentario.Find(id);
            db.comentario.Remove(comentario);
            db.SaveChanges();
            aux2 = comentario.id_entrada;
            return RedirectToAction("../blogs/Details/" + aux2);
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
