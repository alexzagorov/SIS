using System;
using System.Threading.Tasks;
using SIS.HTTP;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new HttpServer(80);
            await server.StartAsync();
        }
    }
}
