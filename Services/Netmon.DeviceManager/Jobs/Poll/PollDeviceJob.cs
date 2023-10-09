using System.Net;
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
    
            return new { Device = deviceDBO, StatusCode = response.StatusCode, ResponseBody = responseBody };
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
    }
}