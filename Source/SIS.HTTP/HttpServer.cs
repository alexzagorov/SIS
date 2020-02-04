using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using static SIS.HTTP.HttpConstants;

namespace SIS.HTTP
{
    public class HttpServer : IHttpServer
    {
        private readonly TcpListener tcpListener;
        private readonly IList<Route> routeTable;
        private readonly IDictionary<string, IDictionary<string, string>> sessions = new Dictionary<string, IDictionary<string, string>>();

        public HttpServer(int port, IList<Route> routeTable)
        {

           
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);
            this.routeTable = routeTable;
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using var tcpStream = tcpClient.GetStream();
            var incomingBytes = new byte[100000];
            await tcpStream.ReadAsync(incomingBytes, 0, incomingBytes.Length);
            string incomingRequestString = Encoding.UTF8.GetString(incomingBytes, 0, incomingBytes.Length);

            try
            {
                var request = new HttpRequest(incomingRequestString);


                var sessionCookie = request.Cookies.FirstOrDefault(x => x.Name == HttpConstants.SessionIdCookieName);
                if (sessionCookie != null && this.sessions.ContainsKey(sessionCookie.Value))
                {
                    request.SessionData = this.sessions[sessionCookie.Value];
                }

                // Sending response
                HttpResponse httpResponse;              
                var route = this.routeTable.Where(x => x.HttpMethod == request.Method && x.Path == request.Path).FirstOrDefault();
                if (route == null)
                {
                    httpResponse = new HttpResponse(HttpResponseCode.NotFound, new byte[0]);
                }
                else
                {
                    httpResponse = route.Action(request);
                }


                httpResponse.Headers.Add(new Header("Server", "SoftUniTestServer/1.1"));

                if (sessionCookie == null || !this.sessions.ContainsKey(sessionCookie.Value))
                {
                    var newSessionId = Guid.NewGuid().ToString();
                    this.sessions.Add(newSessionId, new Dictionary<string, string>());


                    httpResponse.Cookies.Add(new ResponseCookie(HttpConstants.SessionIdCookieName, newSessionId) 
                    { 
                        HttpOnly = true, 
                        MaxAge = 30*3600 
                    });

                }

                var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

                await tcpStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                await tcpStream.WriteAsync(httpResponse.Body, 0, httpResponse.Body.Length);

                Console.WriteLine(incomingRequestString);
                Console.WriteLine(new string('=', 50));
            }
            catch (Exception ex)
            {
                var errorBody = Encoding.UTF8.GetBytes($@"<h1>Something went wrong</h1> 
                                                          <h2>{ex}</h2>");

                var errorResponse = new HttpResponse(HttpResponseCode.InternalServerError, errorBody);

                errorResponse.Headers.Add(new Header("Content-Type", "text/html"));
                errorResponse.Headers.Add(new Header("Server", "SoftUniTestServer/1.1"));

                var errorResponseBytes = Encoding.UTF8.GetBytes(errorResponse.ToString());

                await tcpStream.WriteAsync(errorResponseBytes, 0, errorResponseBytes.Length);
                await tcpStream.WriteAsync(errorResponse.Body, 0, errorResponse.Body.Length);
            }
        }

        public async Task ResetAsync()
        {
            this.Stop();
            this.StartAsync();
        }

        public async Task StartAsync()
        {
            tcpListener.Start();

            while (true)
            {
                // Reading the request

                var tcpClient = await tcpListener.AcceptTcpClientAsync();
                Task.Run(() => ProcessClientAsync(tcpClient));
            }
        }

        public void Stop()
        {
            this.tcpListener.Stop();
        }
    }
}
