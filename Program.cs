using System;
using WebSocketSharp.Server;

namespace Printservice
{
    class Program
    {
        static WebSocketServer wssv;
        static string serverStr = "ws://localhost:6543";
        static void Main(string[] args)
        {
            ConnectWebSocket();
        }
        static void ConnectWebSocket()
        {
            wssv = new WebSocketServer(serverStr);
            wssv.AddWebSocketService<PrintServer>("/print");
            wssv.Start();
            ShowMessage("Server started --- (" + serverStr + ")");
        }

        static void DisconnectWebSocket()
        {
            wssv.Stop();
            ShowMessage("Server Stopped...");
        }

        static void ShowMessage(string msg)
        {

            do
            {
                Console.Clear();
                Console.WriteLine(msg);
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Press (1) to Start The Server");
                Console.WriteLine("Press (2) to Stop The Server");
                Console.WriteLine("Press (3) to Exit The Server");
                Console.WriteLine("--------------------------------");
                var ch = Console.ReadLine();
                if (ch == "1")
                {
                    ConnectWebSocket();
                }
                else if (ch == "2")
                {
                    DisconnectWebSocket();
                }
                else if (ch == "3")
                {
                    Console.WriteLine("Are You Sure To Exit? (y/n)");
                    var ext = Console.ReadLine();
                    if (ext.ToLower() == "y")
                    {
                        return; //exit the application
                    }
                }

            } while (1 == 1);

        }
    }
}
