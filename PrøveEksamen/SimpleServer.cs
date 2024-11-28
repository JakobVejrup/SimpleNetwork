using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class SimpleServer
{
    private bool firstEven = true; // Sporer om det er første gang et lige tal modtages
    private bool firstOdd = true;  // Sporer om det er første gang et ulige tal modtages

    public void Start()
    {
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 6010); // Start serveren på port 6010
        server.Start();
        Console.WriteLine("[Server] Venter på forbindelse...");

        using (TcpClient client = server.AcceptTcpClient()) // Accepter klientforbindelse
        {
            Console.WriteLine("[Server] Klient forbundet");

            using (StreamReader reader = new StreamReader(client.GetStream()))
            using (StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true })
            {
                string line;
                while ((line = reader.ReadLine()) != null) // Læs besked fra klienten
                {
                    Console.WriteLine("[Server] Modtaget: " + line);
                    try
                    {
                        int number = int.Parse(line); // Konverter input til et heltal
                        string response;

                        if (number % 2 == 0) // Lige tal
                        {
                            if (firstEven)
                            {
                                response = "lige";
                                firstEven = false;
                            }
                            else
                            {
                                response = "igen lige";
                            }
                        }
                        else // Ulige tal
                        {
                            if (firstOdd)
                            {
                                response = "ulige";
                                firstOdd = false;
                            }
                            else
                            {
                                response = "igen ulige";
                            }
                        }

                        writer.WriteLine(response); // Send svar til klienten
                        Console.WriteLine("[Server] Sendt: " + response);
                    }
                    catch (FormatException)
                    {
                        writer.WriteLine("Ugyldigt tal"); // Hvis input ikke er et gyldigt tal
                    }
                }
            }
        }

        server.Stop(); // Luk serveren
        Console.WriteLine("[Server] Lukket");
    }
}
