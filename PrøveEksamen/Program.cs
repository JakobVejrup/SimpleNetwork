using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        SimpleServer server = new SimpleServer();
        Thread serverThread = new Thread(server.Start);
        serverThread.Start(); // Start serveren i en separat tråd

        Thread.Sleep(1000); // Vent på, at serveren starter

        SimpleClient client = new SimpleClient();
        client.Start(); // Start klienten
    }
}
