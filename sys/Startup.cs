using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(sys.Startup))]

namespace sys
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

            //網路版本
            //app.UseCors(CorsOptions.AllowAll);
            //app.MapSignalR(new HubConfiguration { EnableJSONP = true });
        }
    }
}
