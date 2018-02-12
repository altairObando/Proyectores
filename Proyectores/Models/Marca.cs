using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyectores.Models
{
    public class Marca
    {
        [Key]
        public int id_marca { get; set; }
        public string marca { get; set; }
        //public virtual List<Proyector> Proyectores { get; set; }
    }
}