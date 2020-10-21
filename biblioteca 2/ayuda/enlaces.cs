using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biblioteca_2.Models;

namespace biblioteca_2.ayuda
{
    public class enlaces
    {
        static Database2 db = new Database2();


        public static bool comprobar(string controlador, string vista)
        {
            string username = HttpContext.Current.User.Identity.Name;

            if (username != "" && username != null)
            {
                username = HttpContext.Current.User.Identity.Name;
                AspNetUsers usr = db.AspNetUsers.Where(a => a.UserName == username).FirstOrDefault();

                permisos permiso = (from m in db.permisos
                                    where m.controlador == controlador && m.vista == vista 
                                    where m.IdRol == usr.IdRol || m.IdRol == "3"
                                    select m).FirstOrDefault();

                if (permiso == null)
                {
                    if (controlador != "home")
                    {
                        return false;
                    }
                }
                else
                {
                    if (controlador != "home")
                    {
                        if (permiso.estado == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}