//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoSMP.Models
{
    using System;
    
    public partial class ConsultarCalendario_Result
    {
        public int IdEvento { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime Comienza { get; set; }
        public Nullable<System.DateTime> Fin { get; set; }
        public string Color { get; set; }
        public Nullable<bool> TodoDia { get; set; }
    }
}
