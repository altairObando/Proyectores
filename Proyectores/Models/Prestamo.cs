using Proyectores.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Proyectores.Models
{
    [DataContract(IsReference = true)]
    public class Prestamo
    {
        [Key]
        public int id_prestamo { get; set; }
        public int id_proyector { get; set; }
        public int id_docente { get; set; }
        public string id_responsable { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }
        public bool? prestamo_anulado { get; set; }
        public bool? finalizado { get; set; }
        public bool activo { get; set; }
        /* Propiedades de navegacion */
        public virtual Proyector Proyector { get; set; }
        public virtual Docente Docente { get; set; }
        public virtual ApplicationUser Responsable { get; set; }
        public virtual List<Devolucion> Devoluciones { get; set; }
    }
}