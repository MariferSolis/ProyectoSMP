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
    
    public partial class Canton
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Canton()
        {
            this.Distrito = new HashSet<Distrito>();
        }
    
        public string Provincia { get; set; }
        public string Canton1 { get; set; }
        public string Nombre { get; set; }
    
        public virtual Provincia Provincia1 { get; set; }
        public virtual Provincia Provincia2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Distrito> Distrito { get; set; }
    }
}
