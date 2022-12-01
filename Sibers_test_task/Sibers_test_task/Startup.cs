using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sibers_test_task.Startup))]
namespace Sibers_test_task
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
