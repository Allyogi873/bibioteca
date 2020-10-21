using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using biblioteca_2.Models;
using biblioteca_2.ayuda;
using System.Data;
using System.Data.Entity;
using System.Net;




namespace biblioteca_2.Controllers
{    
    public class BlogsController : Controller
    {
        private Database2 db = new Database2();
        private actualizar esta = new actualizar();
       
        // GET: Blogs
        public ActionResult Index(int? page)
        {
            
            var pageNumber = page ?? 1;

            var entradas = (from m in db.entrada
                            where m.estado.Contains("1")
                            orderby m.fecha_entrada descending
                            select m).ToList();

            var unaPagina = entradas.ToPagedList(pageNumber, 5);
            ViewBag.pagina = unaPagina;

            var pageNumber2 = page ?? 1;

            var entradas2 = (from n in db.entrada
                            orderby n.fecha_entrada descending
                            select n).ToList();

            var unaPagina2 = entradas2.ToPagedList(pageNumber2, 5);
            ViewBag.pagina2 = unaPagina2;


            return View();
        }


        public ActionResult IndexLibro(int? page, string busqueda)
        {

            var pageNumber = page ?? 1;


            var query = (from n in db.docs
                             orderby n.nombre ascending
                             select n).ToList();

            if (busqueda != "" && busqueda != null && busqueda != "RammsteinRosenrot" && busqueda != "RammsteinRosenrot2")
            {
                query = query.Where(a => a.nombre.ToLower().Contains(busqueda.ToLower()) || a.autor.ToLower().Contains(busqueda.ToLower())).ToList();


            }
            if (busqueda == "RammsteinRosenrot" )
            {
                query = query.OrderByDescending(a => a.autor).ToList();
            }
            if (busqueda == "RammsteinRosenrot2")
            {
                query = query.OrderByDescending(a => a.categoria).ToList();
               
            }

            var unaPagina = query.ToPagedList(pageNumber, 5);
            ViewBag.pagina = unaPagina;
            return View();
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int id)
        {
            var entrada1 = db.entrada.Include(e => e.AspNetUsers).Include(e => e.docs);
            entrada entrada = db.entrada.Find(id);

            ViewBag.id_archivo = new SelectList(db.docs, "id_archivo", "");
            if (entrada == null)
            {
                return HttpNotFound();
            }         
            return View(entrada);
        }

        public JsonResult getLibro(String busqueda)
        {
            var query = from m in db.docs
                        where m.nombre.Contains(busqueda) || m.autor.Contains(busqueda)
                        select new { m.id_archivo, m.nombre, m.dir_archivo, m.categoria, m.autor };


            return Json(query.ToList(), JsonRequestBehavior.AllowGet);
        }



        // GET: Blogs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Blogs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blogs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
