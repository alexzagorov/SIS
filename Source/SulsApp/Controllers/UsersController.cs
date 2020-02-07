using SIS.HTTP;
using SIS.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SulsApp.Controllers
{
    public class UsersController
    {
        public HttpResponse Login(HttpRequest request)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var html = File.ReadAllText("Views/Users/Login.html");

            layout = layout.Replace("@RenderBody()", html);

            return new HtmlResponse(layout);
        }

        public HttpResponse Register(HttpRequest request)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var html = File.ReadAllText("Views/Users/Register.html");

            layout = layout.Replace("@RenderBody()", html);

            return new HtmlResponse(layout);
        }
    }
}
