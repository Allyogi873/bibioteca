//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace biblioteca_2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class permisos
    {
        public int cod_permiso { get; set; }
        public string controlador { get; set; }
        public string vista { get; set; }
        public string IdRol { get; set; }
        public int estado { get; set; }
    
        public virtual AspNetRoles AspNetRoles { get; set; }
    }
}
