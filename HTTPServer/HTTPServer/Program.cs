using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string NewHTTPLine = "\r\n";
            var tcpListener = new TcpListener(IPAddress.Loopback ,80);
   
            tcpListener.Start();

            while (true)
            {
                // Reading the request

                var tcpClient = await tcpListener.AcceptTcpClientAsync();
                using var tcpStream = tcpClient.GetStream();
                var incomingBytes = new byte[100000];
                await tcpStream.ReadAsync(incomingBytes, 0, incomingBytes.Length);
                string incomingRequestString = Encoding.UTF8.GetString(incomingBytes, 0, incomingBytes.Length);

                // Sending response

                string responseBody = "<h1>" + "" + "</h1>" + "<h1>" + DateTime.UtcNow + "</h1>";
                string response = "HTTP/1.1 200 OK" + NewHTTPLine +
                    "Content-Type: text/html" + NewHTTPLine +
                    "Server: SoftUniTestServer/1.1" + NewHTTPLine +
                    "Set-Cookie: user=Alex; Max-Age=3600; HttpOnly" + NewHTTPLine +
                    "Content-Lenght: " + responseBody.Length + NewHTTPLine +
                    NewHTTPLine +
                    responseBody;

                var responseBytes = Encoding.UTF8.GetBytes(response);

                Thread.Sleep(5000);

                await tcpStream.WriteAsync(responseBytes, 0, responseBytes.Length);

                Console.WriteLine(incomingRequestString);
                Console.WriteLine(new string('=', 50));
            }
        }
    }
}
