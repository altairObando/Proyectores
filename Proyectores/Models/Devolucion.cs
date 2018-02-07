using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyectores.Models
{
    /// <summary>
    /// Tabla que permite registrar las devoluciones registradas 
    /// </summary>
    public class Devolucion
    {
        [Key]
        public int id_devolucion { get; set; }
        public int id_prestamo { get; set; }
        public TimeSpan hora { get; set; }
        public bool? anulada { get; set; }
        //Propiedad de navegacion
        public virtual Prestamo Prestamo { get; set; }
    }
}