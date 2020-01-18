using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
                var tcpClient = tcpListener.AcceptTcpClient();
                using var incomingStream = tcpClient.GetStream();
                var incomingBytes = new byte[100000];
                incomingStream.Read(incomingBytes, 0, incomingBytes.Length);
                string incomingRequestString = Encoding.UTF8.GetString(incomingBytes, 0, incomingBytes.Length);

                Console.WriteLine(incomingRequestString);
                Console.WriteLine(new string('=', 50));
            }
        }
    }
}
