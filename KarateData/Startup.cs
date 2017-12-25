using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KarateData.Startup))]
namespace KarateData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
