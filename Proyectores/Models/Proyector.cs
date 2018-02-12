using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyectores.Models
{
   
    public class Proyector
    {

        [Key]
        public int id_proyector { get; set; }
        public int id_marca { get; set; }
        public int id_estado { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        /* Navegacion */
        public virtual Marca Marca { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual List<Prestamo> Prestamos { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", Marca.marca, nombre);
        }

    }
    public class Estado
    {
        [Key]
        public int id_estado { get; set; }
        public string nombre { get; set; }
        public virtual List<Proyector> Proyectores { get; set; }
        public override string ToString()
        {
            return nombre;
        }
    }
}