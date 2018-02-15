using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyectores.Models
{
    public class Departamento
    {
        [Key]
        public int id_departamento { get; set; }
        [Required(ErrorMessage = " Complete!!! Nombre del Departamento?")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 7)]
        [Display(Name = "Nombre del Departamento")]
        public string ConfirmNewPassword { get; set; }
        public string nombre { get; set; }
        public string mision { get; set; }
        public string vision { get; set; }
        public string historia { get; set; }
        public string ubicacion { get; set; }
        public string organizacion { get; set; }

        public override string ToString()
        {
            return this.nombre;
        }

    }
}