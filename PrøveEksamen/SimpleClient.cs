using System;
using System.IO;
using System.Net.Sockets;

class SimpleClient
{
    public void Start()
    {
        try
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 6010)) // Forbind til serveren
            {
                Console.WriteLine("[Client] Forbindelse åbnet");

                using (StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true })
                using (StreamReader reader = new StreamReader(client.GetStream()))
                {
                    string[] numbers = { "82", "19", "43", "2", "61" }; // Liste over tal

                    foreach (string number in numbers)
                    {
                        Console.WriteLine("[Client] Sender: " + number);
                        writer.WriteLine(number); // Send tal til serveren

                        string response = reader.ReadLine(); // Læs svar fra serveren
                        Console.WriteLine("[Client] Modtaget: " + response);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("[Client] Fejl: " + e.Message);
        }
    }
}
