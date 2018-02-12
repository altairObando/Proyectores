using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyectores.Models
{
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