using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Response
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string newLocatin)
        {
            this.Headers.Add(new Header("Location", newLocatin));
            this.Code = HttpResponseCode.Found;
        }
    }
}
