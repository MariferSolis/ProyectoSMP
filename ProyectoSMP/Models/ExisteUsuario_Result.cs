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
    using System.ComponentModel.DataAnnotations;

    public partial class ExisteUsuario_Result
    {
        public int IdUsuario { get; set; }
        public string Identificacion { get; set; }
        public int IdTipoDeIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public string TipoCarga { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public int IdRol { get; set; }
        public bool Estado { get; set; }
        public bool Recordarme { get; set; }
        public string token_recovery { get; set; }
    }
}
