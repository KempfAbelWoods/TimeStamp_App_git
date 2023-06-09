namespace TimeStamp_App.Connection
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    public class Client
    {
        private const int Port = 8888;

        public static void Main()
        {
            string serverIpAddress = "127.0.0.1";

            // Erstelle eine TCP/IP-Verbindung
            TcpClient client = new TcpClient();
            client.Connect(serverIpAddress, Port);

            Console.WriteLine("Verbunden mit Server.");

            // Erstelle ein NetworkStream-Objekt für die Kommunikation
            NetworkStream networkStream = client.GetStream();

            // Sende Daten an den Server
            string message = "Hallo vom Client!";
            byte[] data = Encoding.ASCII.GetBytes(message);
            networkStream.Write(data, 0, data.Length);

            // Empfange die Antwort vom Server
            byte[] buffer = new byte[1024];
            int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Console.WriteLine("Antwort empfangen: " + response);

            // Schließe die Verbindung
            client.Close();

            Console.WriteLine("Verbindung geschlossen.");
        }
    }

    }
}