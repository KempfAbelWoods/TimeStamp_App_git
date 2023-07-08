using System.Diagnostics;
using Newtonsoft.Json;
using TimeStamp_App.Ansichten;
using TimeStamp_App.DB;
using TimeStamp_App.Helper;

namespace TimeStamp_App.Connection
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    public class Client
    {
        private const int Port = 8080;

        public async static void SocketClientCode(string senddata)
        {
            byte[] buffer = new byte[1024];
            byte[] data;
            int bytesRead;
            string response;
            string message;
            string serverIpAddress = "192.168.2.110";

            // Erstelle eine TCP/IP-Verbindung
            TcpClient client = new TcpClient();
            client.Connect(serverIpAddress, Port);

            Trace.WriteLine("Verbunden mit Server.");

            // Erstelle ein NetworkStream-Objekt für die Kommunikation
            NetworkStream networkStream = client.GetStream();

            // Sende Daten an den Server
            message = Config.ConnectionCode;
            data = Encoding.ASCII.GetBytes(message);
            networkStream.Write(data, 0, data.Length);

            // Empfange Bestätigung für Senden
            bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
            response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            // Senden von Tasks
            if (response == "Freigabe")
            {
                var (taskList, error) = Rw_Tasks.Read("", Paths.sqlite_path);
                string jsonString = JsonConvert.SerializeObject(taskList);
                data = Encoding.ASCII.GetBytes(jsonString);
                networkStream.Write(data,0,data.Length);
            }


            // Empfange die Antwort vom Server
            bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
            response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Trace.WriteLine("Antwort empfangen: " + response);

            var lists = ConnectionHelper.DeSerializeList(response);
            ConnectionHelper.RW_Users_Tasks(lists);

            // Schließe die Verbindung
            client.Close();

            Trace.WriteLine("Verbindung geschlossen.");
        }
        
        public async static void SocketClientUser(string senddata)
        {
            byte[] buffer = new byte[1024];
            byte[] data;
            int bytesRead;
            string response;
            string message;
            string serverIpAddress = "192.168.2.110";
            
            // Erstelle eine TCP/IP-Verbindung
            TcpClient client = new TcpClient();
            client.Connect(serverIpAddress, Port);

            Trace.WriteLine("Verbunden mit Server.");

            // Erstelle ein NetworkStream-Objekt für die Kommunikation
            NetworkStream networkStream = client.GetStream();

            // Sende Daten an den Server
            var (list, err) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err != null)
            {
                Trace.WriteLine(err.GetException().Message);
            }
            message = list[0].Ressource;
            data = Encoding.ASCII.GetBytes(message);
            networkStream.Write(data, 0, data.Length);

            // Empfange Bestätigung für Senden
            bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
            response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            // Senden von Tasks
            if (response == "Freigabe")
            {
                var (taskList, error) = Rw_Tasks.Read("", Paths.sqlite_path);
                string jsonString = JsonConvert.SerializeObject(taskList);
                data = Encoding.ASCII.GetBytes(jsonString);
                networkStream.Write(data,0,data.Length);
            }
            else
            {
                Trace.WriteLine("Ungültiger User");
            }


            // Empfange die Antwort vom Server
            bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
            response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Trace.WriteLine("Antwort empfangen: " + response);

            var lists = ConnectionHelper.DeSerializeList(response);
            ConnectionHelper.RW_Users_Tasks(lists);

            // Schließe die Verbindung
            client.Close();

            Trace.WriteLine("Verbindung geschlossen.");
        }
    }
}