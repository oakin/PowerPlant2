using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace WebSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            var ws = new WebSocket(url: "ws://localhost:8888/ws");
            ws.Connect();
            ws.Send("Hey, Server!");
            ws.OnMessage += OnMessage;
            ws.OnError += OnError;
                 
            Console.ReadKey(true);
            ws.Close();
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            Console.Write("Error: {0}, Exception: {1}", e.Message, e.Exception);
        }

        private static void OnMessage(object sender, MessageEventArgs e)
        {
            Console.Write("Message received: {0}", e.Data.ToString());
        }

       

        
    }
}
