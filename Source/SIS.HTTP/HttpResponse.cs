using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpResponseCode statusCode, byte[] body)
            :this()
        {
            this.Version = HttpVersionType.Http11;
            this.Code = statusCode;
            this.Body = body;   
           
            if (body.Length > 0 && body != null)
            {
                this.Headers.Add(new Header("Content-Length", body.Length.ToString()));
            }
        }

        internal HttpResponse()
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<ResponseCookie>();
        }

        public HttpVersionType Version { get; set; }

        public HttpResponseCode Code { get; set; }

        public IList<ResponseCookie> Cookies { get; set; }

        public IList<Header> Headers { get; set; }

        public byte[] Body { get; set; }

        public override string ToString()
        {
            var responseBuilder = new StringBuilder();

            var httpVerionAsString = this.Version.ToString() switch
            {
                "Http10" => "HTTP/1.0",
                "Http11" => "HTTP/1.1",
                "Http20" => "HTTP/2.0",
                _ => "HTTP/1.1",
            };

            responseBuilder.Append($"{httpVerionAsString} {(int)this.Code} {this.Code.ToString()}" + HttpConstants.NewLine);

            foreach (var header in this.Headers)
            {
                responseBuilder.Append(header.ToString() + HttpConstants.NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                responseBuilder.Append($"Set-Cookie: {cookie.ToString()}" + HttpConstants.NewLine);
            }

            responseBuilder.Append(HttpConstants.NewLine);

            return responseBuilder.ToString();
        }
    }
}
