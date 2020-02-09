using SIS.HTTP;
using SIS.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        protected HttpResponse View([CallerMemberName]string viewName = null)
        {
            var controllerName = this.GetType().Name.Replace("Controller", string.Empty);

            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var html = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            layout = layout.Replace("@RenderBody()", html);

            return new HtmlResponse(layout);
        }
    }
}
