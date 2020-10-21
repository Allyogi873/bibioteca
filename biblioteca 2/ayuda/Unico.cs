using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using biblioteca_2.Models;

namespace biblioteca_2.ayuda
{
    public class Unico
    {
        static Database2 db = new Database2();
        public static bool usuarioo(string userName)
        {
            foreach (var item in db.AspNetUsers)
            {
                if (item.usuario == userName)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        public static bool rango(string dir)
        {
            if (dir.Length <= 5 || dir.Length >= 20 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}