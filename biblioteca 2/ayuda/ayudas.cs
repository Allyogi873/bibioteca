using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biblioteca_2.Models;

namespace biblioteca_2.ayuda
{
    public class ayudas
    {
        static Database2 db = new Database2();

        public static bool compobar(string estados)
        {
          
            if (estados != "" && estados != null) { 

                if (estados == null)
                {
                    
                        return false;
                    
                }
                else
                {
                    
                        if (estados == "2")
                        {
                            return false;
                        }
                   
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static string ExtraerUs()
        {
            string username = HttpContext.Current.User.Identity.Name;
            
            foreach (var item in db.AspNetUsers){
                if(item.UserName == username || item.Email == username)
                {
                    username = item.Id;
                }
            }
            return username ;
        }
/*
        public static string libro(int id)
        {
            string ellibroes = "";
            foreach (var item in db.entrada){
                if (item.id_archivo == id){
                    id = item.id_archivo;
                }
            }
            foreach (var item in db.docs)
            {
                if(item.i==id){
                    ellibroes = item.dir_archivo;
                }
            }
            return ellibroes;
        }
        */
    }
}