using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SulsApp
{
    public class StartUp : IMvcApplication
    {
        public void ConfigureServices()
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
        }

        public void Configure(IList<Route> routeTable)
        {
            routeTable.Add(new Route("/", HttpMethod.GET, new HomeController().Index));
            routeTable.Add(new Route("/Users/Login", HttpMethod.GET, new UsersController().Login));
            routeTable.Add(new Route("/Users/Register", HttpMethod.GET, new UsersController().Register));

            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.GET, new StaticFilesController().Bootstrap));
            routeTable.Add(new Route("/css/site.css", HttpMethod.GET, new StaticFilesController().Site));
            routeTable.Add(new Route("/css/reset.css", HttpMethod.GET, new StaticFilesController().Reset));
        }
    }
}
