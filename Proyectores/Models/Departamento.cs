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
        public string nombre { get; set; }
        //Lo unico requerido era que se guardara el nombre del departamento, en la bd anterior
        //[Required(ErrorMessage = " Complete!!! Misión del Departamento?")]
        [DataType(DataType.Text)]
        [Display(Name = "Misión")]
        public string mision { get; set; }

        //[Required(ErrorMessage = " Complete!!! Visión del Departamento?")]
        [DataType(DataType.Text)]
        [Display(Name = "Visión")]
        public string vision { get; set; }

        //[Required(ErrorMessage = " Complete!!! Historia del Departamento?")]
        [DataType(DataType.Text)]
        [Display(Name = "Historia")]
        public string historia { get; set; }

        //[Required(ErrorMessage = " Complete!!! Ubicación del Departamento?")]
        [DataType(DataType.Text)]
        [Display(Name = "Ubicación")]
        public string ubicacion { get; set; }

        //[Required(ErrorMessage = " Complete!!! Organización del Departamento?")]
        [DataType(DataType.Text)]
        [Display(Name = "Organización")]        
        public string organizacion { get; set; }

        public override string ToString()
        {
            return this.nombre;
        }

    }
}