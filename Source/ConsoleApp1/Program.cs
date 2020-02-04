using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SIS.HTTP;
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
            throw new NotImplementedException();
            //response.Headers.Add(new Header("Content-Type", "text/html"));

        }

        private static HttpResponse Contact(HttpRequest request)
        {
            var responseBodyContent = "<h1>Contact</h1>";

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseBodyContent);

            var response = new HttpResponse(HttpResponseCode.OK, responseBodyBytes);

            response.Headers.Add(new Header("Content-Type", "text/html"));

            return response;
        }

        public static HttpResponse Index(HttpRequest request)
        {
            var responseBodyContent = "<h1>Sweet Home</h1>";

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseBodyContent);

            var response = new HttpResponse(HttpResponseCode.OK, responseBodyBytes);

            response.Headers.Add(new Header("Content-Type", "text/html"));

            return response;
        }

        public static HttpResponse Login(HttpRequest request)
        {
            var responseBodyContent = "<h1>Login Page</h1>";

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseBodyContent);

            var response = new HttpResponse(HttpResponseCode.OK, responseBodyBytes);

            response.Headers.Add(new Header("Content-Type", "text/html"));

            return response;
        }

        public static HttpResponse DoLogin(HttpRequest request)
        {
            var responseBodyContent = "<h1>Login Page</h1>";

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseBodyContent);

            var response = new HttpResponse(HttpResponseCode.OK, responseBodyBytes);

            response.Headers.Add(new Header("Content-Type", "text/html"));

            return response;
        }
    }
}
