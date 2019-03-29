using Quobject.Collections.Immutable;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socket_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new IO.Options { IgnoreServerCertificateValidation = true, AutoConnect = true, ForceNew = true };
            options.Transports = ImmutableList.Create<string>("websocket");
            
            var socket = IO.Socket("http://127.0.0.1:3030", options);

            socket.On(Socket.EVENT_ERROR, (error) =>
            {
                Console.WriteLine("Generic error {0}", error);
            });

            socket.On(Socket.EVENT_CONNECT_ERROR, (error) =>
            {

                Console.WriteLine("Connect error {0}", error);
            });

            socket.On(Socket.EVENT_CONNECT, (data) =>
            {
                Console.WriteLine("Connected {0}", data);
            });

            socket.On(Socket.EVENT_MESSAGE, (message) =>
            {
                Console.WriteLine("Message arrived {0}", message);
            });

            socket.On("channel created", (message) =>
            {
                Console.WriteLine("Message arrived for create new {0}", message);
            });

            socket.On("channel updated", (message) =>
            {
                Console.WriteLine("Message arrived for updated data {0}", message);
            });

            Console.ReadKey();
        }
    }
}
