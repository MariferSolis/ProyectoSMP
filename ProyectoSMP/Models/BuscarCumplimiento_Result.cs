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
    
    public partial class BuscarCumplimiento_Result
    {
        public int IdCumplimiento { get; set; }
        public string Mantenimiento { get; set; }
        public System.DateTime Comienza { get; set; }
        public System.DateTime Finaliza { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string Detalles { get; set; }
        public string Color { get; set; }
    }
}