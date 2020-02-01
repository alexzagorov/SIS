using System;
using System.Collections.Generic;
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

        public HttpServer(int port)
        {

           
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);

        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using var tcpStream = tcpClient.GetStream();
            var incomingBytes = new byte[100000];
            await tcpStream.ReadAsync(incomingBytes, 0, incomingBytes.Length);
            string incomingRequestString = Encoding.UTF8.GetString(incomingBytes, 0, incomingBytes.Length);
            var request = new HttpRequest(incomingRequestString);

            // Sending response

            string responseBody = "<h1>" + "" + "</h1>" + "<h1>" + DateTime.UtcNow + "</h1>";
            var httpResponse = new HttpResponse(HttpResponseCode.OK, Encoding.UTF8.GetBytes(responseBody));
            httpResponse.Headers.Add(new Header("Content-Type", "text/html"));
            httpResponse.Headers.Add(new Header("Server", "SoftUniTestServer/1.1"));

            var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

            await tcpStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            await tcpStream.WriteAsync(httpResponse.Body, 0, httpResponse.Body.Length);

            Console.WriteLine(incomingRequestString);
            Console.WriteLine(new string('=', 50));

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
