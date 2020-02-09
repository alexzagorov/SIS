using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP;
using System.Threading.Tasks;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static async Task StartAsync(IMvcApplication application)
        {
            var routeTable = new List<Route>();
            application.ConfigureServices();
            application.Configure(routeTable);

            var server = new HttpServer(80, routeTable);
            await server.StartAsync();
        }
    }
}
