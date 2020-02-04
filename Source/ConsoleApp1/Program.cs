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
            var dbContext = new ApplicationDbContext();
            dbContext.Database.EnsureCreated();

            var routeTable = new List<Route>();
            routeTable.Add(new Route("/", HttpMethod.GET, Index));
            routeTable.Add(new Route("/Tweets/Create", HttpMethod.POST, CreateTweet));

            routeTable.Add(new Route("/favicon.ico", HttpMethod.GET, FavIcon));


            var server = new HttpServer(80, routeTable);
            await server.StartAsync();
        }

        private static HttpResponse CreateTweet(HttpRequest request)
        {
            var db = new ApplicationDbContext();
            db.Tweets.Add(new Tweet()
            {
                CreatedOn = DateTime.UtcNow,
                Creator = request.FormData["creator"],
                Content = request.FormData["tweetName"]
            });
            db.SaveChanges();

            return new RedirectResponse("/");
        }

        private static HttpResponse FavIcon(HttpRequest request)
        {
            var iconBytes = File.ReadAllBytes("wwwroot/profilePic.ico");

            return new FileResponse(iconBytes, "image/x-icon");
        }
       
        public static HttpResponse Index(HttpRequest request)
        {
            return new HtmlResponse("<form action=/Tweets/Create method='post'><input type='text' name='creator'> <br> <textarea name='tweetName'></textarea> <br> <input type='submit' /></form>");
        }
    }
}
