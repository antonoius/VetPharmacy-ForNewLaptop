using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VetPharmacy.Startup))]
namespace VetPharmacy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
