using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var routeTable = new List<Route>();

            routeTable.Add(new Route("/", HttpMethod.GET, Index));
            routeTable.Add(new Route("/users/login", HttpMethod.GET, Login));
            routeTable.Add(new Route("/users/login", HttpMethod.POST, DoLogin));
            routeTable.Add(new Route("/contact", HttpMethod.GET, Contact));
            routeTable.Add(new Route("/favicon.ico", HttpMethod.GET, FavIcon));


            var server = new HttpServer(80, routeTable);
            await server.StartAsync();
        }

        private static HttpResponse FavIcon(HttpRequest request)
        {
            var iconBytes = File.ReadAllBytes("wwwroot/profilePic.ico");

            return new FileResponse(iconBytes, "image/x-icon");
        }

        private static HttpResponse Contact(HttpRequest request)
        {
            return new HtmlResponse("<h1>Contact</h1>");
        }

        public static HttpResponse Index(HttpRequest request)
        {
            return new HtmlResponse("<h1>Sweet Home</h1>");
        }

        public static HttpResponse Login(HttpRequest request)
        {
            return new HtmlResponse("<h1>Login Page</h1>");
        }

        public static HttpResponse DoLogin(HttpRequest request)
        {
            return new HtmlResponse("<h1>Login Page</h1>");
        }
    }
}
