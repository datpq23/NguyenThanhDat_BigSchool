using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NguyenThanhDat_BigSchool.Startup))]
namespace NguyenThanhDat_BigSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
