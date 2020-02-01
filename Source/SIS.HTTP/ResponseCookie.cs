using System;
using System.Text;

namespace SIS.HTTP
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value)
            :base(name, value)
        {
            this.Path = "/";
            this.SameSite = SameSiteType.None;
            this.Expires = DateTime.UtcNow.AddDays(30); 
        }

        public string Domain { get; set; }

        public string Path { get; set; }

        public DateTime? Expires { get; set; }

        public long? MaxAge { get; set; }

        public bool Secure { get; set; }

        public bool HttpOnly { get; set; }

        public SameSiteType SameSite { get; set; }

        public override string ToString()
        {
            var cookieBuilder = new StringBuilder();


            return $"Set-Cookie: ";
        }
    }
}
