using System.Text;
using DeviceManagerService.Util;
using DevicesLib.Database;
using DevicesLib.DBO.Device;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeviceManagerService.Jobs.Poll;

public class PollDeviceJob : IPollDeviceJob
{
    private readonly DevicesDatabase _database;
    
    public PollDeviceJob(DevicesDatabase database)
    {
        _database = database;
    }
    
    public async Task Execute()
    {
        string url = URLHandler.GetSNMPPollingURL("discover/device");
        
        foreach (DeviceDBO deviceDBO in _database.Devices.Include(d => d.DeviceConnection))
        {
            HttpClient client = new();
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
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
    }
}