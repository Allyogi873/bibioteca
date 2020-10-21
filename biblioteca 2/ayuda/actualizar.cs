using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biblioteca_2.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;


namespace biblioteca_2.ayuda
{
    public class actualizar
    {
        static Database2 db = new Database2();
        public static void estados(entrada entrada)
        {
            entrada original = db.entrada.Find(entrada.cod_entrada);
            if (entrada.fecha_publicacion <= DateTime.Now.Date && entrada.contenido.Length > 100 && entrada.portada != null )
            {
                original.estado = "1"; // publicado
            }
            else
            {
                original.estado = "2"; //no publicaco
            }
            db.Entry(original).State = EntityState.Modified;
            db.SaveChanges();

        }
    }



}