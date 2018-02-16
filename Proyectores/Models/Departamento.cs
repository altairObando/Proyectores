using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display (Name ="Departamento")]        
       // public string ConfirmNewPassword { get; set; }
        public string nombre { get; set; }
        [Display(Name = "Misión")]
        public string mision { get; set; }
        [Display(Name = "Visión")]
        public string vision { get; set; }
        [Display(Name = "Historia")]
        public string historia { get; set; }
        [Display(Name = "Ubicación")]
        public string ubicacion { get; set; }
        [Display(Name = "Organización")]
        public string organizacion { get; set; }

        public override string ToString()
        {
            return this.nombre;
        }

    }
}