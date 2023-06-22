﻿using System.Diagnostics;

namespace TimeStamp_App.Connection
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    public class Client
    {
        private const int Port = 8080;
        public static string Ausgabe = "";

        public Client(string enteredUser, string enteredPassword)
        {
            string User = enteredUser;
            string Password = enteredPassword;
        }

        public static void SocketClient()
        {
            string serverIpAddress = "192.168.2.110";

            // Erstelle eine TCP/IP-Verbindung
            TcpClient client = new TcpClient();
            client.Connect(serverIpAddress, Port);

            Trace.WriteLine("Verbunden mit Server.");

            // Erstelle ein NetworkStream-Objekt für die Kommunikation
            NetworkStream networkStream = client.GetStream();
            // Sende Daten an den Server
            string message = "1234";
            byte[] data = Encoding.ASCII.GetBytes(message);
            networkStream.Write(data, 0, data.Length);

            // Empfange die Antwort vom Server
            byte[] buffer = new byte[1024];
            int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Trace.WriteLine("Antwort empfangen: " + response);

            // Schließe die Verbindung
            client.Close();

            Trace.WriteLine("Verbindung geschlossen.");
            Ausgabe = "Verbindung geschlossen";
        }
    }
    
}