using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biblioteca_2.Models
{
    public class Entr_Come
    {
        Database2 db = new Database2();
        public entrada mostrar(string nombre)
        {
            try
            {
                var info = from en in db.entrada
                           join co in db.comentario on en.cod_entrada equals co.id_entrada
                           where en.titulo == nombre
                           select en;
                return info.FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}