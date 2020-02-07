using SIS.HTTP;
using SIS.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SulsApp.Controllers
{
    public class HomeController
    {
        public HttpResponse Index(HttpRequest request)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var html = File.ReadAllText("Views/Home/Index.html");

            layout = layout.Replace("@RenderBody()", html);

            return new HtmlResponse(layout);
        }
    }
}
