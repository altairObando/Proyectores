using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyectores.Models
{
    public class Docente
    {
        [Key]
        public int DocenteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        //Llaves Foraneas
        public int id_especialidad { get; set; }    
        public int id_departamento { get; set; }
        //Propiedades de navegacion
        public virtual Especialidad Especialidad { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual List<Prestamo> Prestamos { get; set; }
    }
}