using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.DeviceManager.Util;

namespace Netmon.DeviceManager.Jobs.Poll;

public class PollDeviceJob : IPollDeviceJob
{
    private readonly DevicesDatabase _database;
    
    public PollDeviceJob(DevicesDatabase database)
    {
        _database = database;
    }
    
    public async Task Execute()
    {
        string url = URLHandler.GetSNMPPollingURL("poll/device");
        
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