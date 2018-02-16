using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Proyectores.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        //Propiedades personalizadas
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public byte[] Foto_Perfil { get; set; }
        ///Fin Propiedades
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Devolucion> Devolucion { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Proyector> Proyectores { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Especialidad> Especialidad { get; set; }

    }
}