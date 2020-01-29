using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP
{
    class HttpServerException: Exception
    {
        public HttpServerException(string message)
            :base(message)
        {
        }
    }
}
