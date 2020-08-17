using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUD_Relations.Startup))]
namespace CRUD_Relations
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
