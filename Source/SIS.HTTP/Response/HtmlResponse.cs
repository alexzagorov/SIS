using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Response
{
    public class HtmlResponse: HttpResponse
    {
        private readonly string htmlAsString;

        public HtmlResponse(string htmlString)
            :base()
        {
            this.Code = HttpResponseCode.OK;

            this.Body = Encoding.UTF8.GetBytes(htmlString);

            this.Headers.Add(new Header("Content-Type", "text/html"));
            this.Headers.Add(new Header("Content-Length", this.Body.Length.ToString()));

        }
    }
}
