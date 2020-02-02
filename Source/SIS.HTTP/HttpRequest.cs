using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string httpStringRequest)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            var lines = httpStringRequest.Split(new string[] { HttpConstants.NewLine }, StringSplitOptions.None);

            //First header line
            var httpInfoHeader = lines[0];
            var infoHeaderParts = httpInfoHeader.Split(' ');
            if (infoHeaderParts.Length != 3)
            {
                throw new HttpServerException("Incorrect HTTP header line!");
            }

            var httpMethod = infoHeaderParts[0];
            this.Method = httpMethod switch
            {
                "GET" => HttpMethod.GET,
                "POST" => HttpMethod.POST,
                "PUT" => HttpMethod.PUT,
                "DELETE" => HttpMethod.DELETE,
                _ => HttpMethod.Unknown,
            };

            this.Path = infoHeaderParts[1];

            var httpVersion = infoHeaderParts[2];
            this.Version = httpVersion switch
            {
                "HTTP/1.0" => HttpVersionType.Http10,
                "HTTP/1.1" => HttpVersionType.Http11,
                "HTTP/2.0" => HttpVersionType.Http20,
                _ => HttpVersionType.Http11,
            };

            //Headers and Body
            bool isHeader = true;
            var bodyBuilder = new StringBuilder();
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    isHeader = false;
                    continue;
                }

                if (isHeader)
                {
                    var headerParts = line.Split(new string[] { ": "}, 2, StringSplitOptions.None);
                    if (headerParts.Length != 2)
                    {
                        throw new HttpServerException($"Invalid header: {line}");
                    }

                    if (headerParts[0] == "Cookie")
                    {
                        var cookies = headerParts[1].Split(new string[] {"; "}, StringSplitOptions.None);
                        foreach (var cookie in cookies)
                        {
                            var cookieParts = cookie.Split(new char[] { '=' }, 2, StringSplitOptions.None);
                            this.Cookies.Add(new Cookie(cookieParts[0], cookieParts[1]));
                        }
                    }
                    else
                    {
                        this.Headers.Add(new Header(headerParts[0], headerParts[1]));
                    }
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }
            }
        }

        public HttpMethod Method { get; set; }

        public string Path { get; set; }

        public HttpVersionType Version { get; set; }

        public IList<Header> Headers { get; set; }

        public IList<Cookie> Cookies { get; set; }

        public string Body { get; set; }
    }
}
