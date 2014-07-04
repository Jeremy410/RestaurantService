using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantService.Startup))]
namespace RestaurantService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
