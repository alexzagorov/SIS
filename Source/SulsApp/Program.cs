using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SIS.HTTP;
using SulsApp.Controllers;

namespace SulsApp
{
    public static class Program
    {
        public static async Task Main()
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            var routeTable = new List<Route>();
            routeTable.Add(new Route("/", HttpMethod.GET, new HomeController().Index));
            routeTable.Add(new Route("/Users/Login", HttpMethod.GET, new UsersController().Login));
            routeTable.Add(new Route("/Users/Register", HttpMethod.GET, new UsersController().Register));

            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.GET, new StaticFilesController().Bootstrap));
            routeTable.Add(new Route("/css/site.css", HttpMethod.GET, new StaticFilesController().Site));
            routeTable.Add(new Route("/css/reset.css", HttpMethod.GET, new StaticFilesController().Reset));

            var server = new HttpServer(80, routeTable);
            await server.StartAsync();
        }
    }
}
