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
    using System.Collections.Generic;
    
    public partial class Mantenimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mantenimiento()
        {
            this.ParoDeMaquina = new HashSet<ParoDeMaquina>();
            this.PlanMantenimiento = new HashSet<PlanMantenimiento>();
        }
    
        public int IdMantenimiento { get; set; }
        public int IdMaquina { get; set; }
        public string Seccion { get; set; }
        public Nullable<int> NumeroOperacion { get; set; }
        public string NombreOperacion { get; set; }
        public Nullable<int> Frecuencia { get; set; }
        public Nullable<int> IdRol { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdRepuesto { get; set; }
        public string Detalles { get; set; }
        public string URLArchivo { get; set; }
    
        public virtual InventarioDeRepuestos InventarioDeRepuestos { get; set; }
        public virtual Maquina Maquina { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParoDeMaquina> ParoDeMaquina { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanMantenimiento> PlanMantenimiento { get; set; }
    }
}