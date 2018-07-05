using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(GeopersonServer.Startup))]
namespace GeopersonServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseCors(CorsOptions.AllowAll);
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfig = new HubConfiguration()
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfig);
            });
            //app.MapSignalR();
            var httpConfig = new HttpConfiguration();
            httpConfig.MapHttpAttributeRoutes();
            app.UseWebApi(httpConfig);
        }
    }
}
