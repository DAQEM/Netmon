using System.Net;
using System.Net.WebSockets;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.DeviceManager.Util;
using Newtonsoft.Json;

namespace Netmon.DeviceManager.Jobs.Poll;

public class PollDeviceJob : IPollDeviceJob
{
    private readonly DevicesDatabase _database;
    private static readonly List<WebSocket> Subscribers = new();
    
    public PollDeviceJob(DevicesDatabase database)
    {
        _database = database;
    }
    
    public async Task Execute()
    {
        string url = UrlHandler.GetSNMPPollingURL("poll/device");
        
        List<DeviceDBO> devices = await _database.Devices
            .Include(d => d.DeviceConnection)
            .ToListAsync();

        var tasks = devices.Select(async deviceDBO =>
        {
            using HttpClient client = new();
            HttpResponseMessage response = await client.PostAsJsonAsync(url, new
            {
                Version = deviceDBO.DeviceConnection.SNMPVersion,
                IpAddress = deviceDBO.IpAddress,
                Port = deviceDBO.DeviceConnection.Port,
                Community = deviceDBO.DeviceConnection.Community,
                AuthPassword = deviceDBO.DeviceConnection.AuthPassword,
                PrivacyPassword = deviceDBO.DeviceConnection.PrivacyPassword,
                AuthProtocol = deviceDBO.DeviceConnection.AuthProtocol,
                PrivacyProtocol = deviceDBO.DeviceConnection.PrivacyProtocol,
                ContextName = deviceDBO.DeviceConnection.ContextName
            });
            string responseBody = await response.Content.ReadAsStringAsync();
    
            return new { Device = deviceDBO, response.StatusCode, ResponseBody = responseBody };
        });

        var results = await Task.WhenAll(tasks);

        foreach (var result in results)
        {
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Device {result.Device.IpAddress} not found.");
            }
            else
            {
                Console.WriteLine(result.ResponseBody);
            }
        }
        
        Console.WriteLine("Polling complete. Notifying {0} subscribers...", Subscribers.Count);
        for (int i = Subscribers.Count - 1; i >= 0; i--)
        {
            WebSocket webSocket = Subscribers[i];
            if (webSocket.State == WebSocketState.Open)
            {
                var json = new { success = true, message = "New Data Available" };
                string jsonString = JsonConvert.SerializeObject(json);
                byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
                
                await webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            else
            {
                Subscribers.Remove(webSocket);
            }
        }
    }
    
    public void Subscribe(WebSocket webSocket)
    {
        Subscribers.Add(webSocket);
    }
}