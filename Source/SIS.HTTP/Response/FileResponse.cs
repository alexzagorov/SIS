using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Response
{
    public class FileResponse: HttpResponse
    {
        public FileResponse(byte[] content, string contentType)
        {
            this.Code = HttpResponseCode.OK;

            this.Body = content;

            this.Headers.Add(new Header("Content-Type", contentType));
            this.Headers.Add(new Header("Content-Length", this.Body.Length.ToString()));
        }
    }
}
