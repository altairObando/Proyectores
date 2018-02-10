using System;
using Microsoft.Owin;
using Owin;
using Proyectores.Models;

[assembly: OwinStartupAttribute(typeof(Proyectores.Startup))]
namespace Proyectores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CrearEspecialidades();
            //crearDepartamentos();
        }

        private void crearDepartamentos()
        {
            var db = new ApplicationDbContext();
            try
            {
                Departamento[] dptos = new Departamento[] {
                new Departamento { nombre = "UNAN - RURD" },
                new Departamento { nombre = "UNAN - RUCFA" },
                new Departamento { nombre = "FAREM - CHONTLES" },
                new Departamento { nombre = "FAREM - ESTELI" },
                new Departamento { nombre = "FAREM - CARAZO" },
                new Departamento { nombre = "UNAN - MATAGALPA" },
                new Departamento { nombre = "UNAN - LEON" }
            };
                db.Departamentos.AddRange(dptos);
                int result = db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                    db.Database.Connection.Close();
            }
        }

        //Agregando Especialidades
        private void CrearEspecialidades()
        {
            var db = new ApplicationDbContext();
            
            try
            {
                var tipo = new string[] { "Ing.","Lic.", "Msc.","Dr." };
                var carrera = new string[]
                {
                    "Ciencias de la Computacion",
                    "Sistemas de la Información",
                    "Telematica",
                    "Redes",
                    "Hardware"
                };
                foreach (var item in tipo)
                {
                    foreach (var item2 in carrera)
                    {
                        var xx = new Especialidad { nombre = item + " " + item2 };
                        db.Especialidad.Add(xx);
                    }
                }
                db.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                    db.Database.Connection.Close();
            }
        }
    }
}
