using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proyectores.Startup))]
namespace Proyectores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        //
    }
}
