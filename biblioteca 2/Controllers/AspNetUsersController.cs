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
using PagedList;
using PagedList.Mvc;

namespace biblioteca_2.Controllers
{
    public class AspNetUsersController : Controller
    {
        private Database2 db = new Database2();

        // GET: AspNetUsers
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var entradas = (from m in db.AspNetUsers
                            orderby m.usuario descending
                            select m).ToList();

            var unaPagina = entradas.ToPagedList(pageNumber, 5);
            ViewBag.pagina = unaPagina;
            var aspNetUsers = db.AspNetUsers.Include(a => a.AspNetRoles);
            return View(aspNetUsers.ToList());
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.AspNetRoles, "Id", "Name");
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,IdRol,fot_perfil,Nombre,Apellido,carne")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRol = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUsers.IdRol);
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUsers.IdRol);
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AspNetUsers aspNetUsers)
        {
            AspNetUsers original = db.AspNetUsers.Find(aspNetUsers.Id);
            
            if (ModelState.IsValid){
                if (Unico.usuarioo(aspNetUsers.usuario)){
                    original.usuario = aspNetUsers.usuario;
                    original.Nombre = aspNetUsers.Nombre;
                    original.Apellido = aspNetUsers.Apellido;
                    original.carne = aspNetUsers.carne;
                    original.PhoneNumber = aspNetUsers.PhoneNumber;

                    if (aspNetUsers.fot_perfil == null)
                    {
                        original.fot_perfil = "defecto.jpg";
                    }
                    
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        var file = Request.Files[0];
                        string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                        original.fot_perfil = archivo;
                        file.SaveAs(Server.MapPath("~/subidas/perfiles/" + archivo));
                    }
                    




                    db.Entry(original).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("../blogs/index/");
                }
            }
            ViewBag.IdRol = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUsers.IdRol);
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
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
