using Microsoft.AspNetCore.Mvc;
using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Memory;
using Netmon.Models.Device;
using Netmon.SNMPPolling.SNMP.Poll.Device;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.Controllers;

[ApiController]
[Route("Discover")]
public class DiscoverController : Controller
{
    private readonly IDevicePoller _devicePoller;
    
    public DiscoverController(IDevicePoller devicePoller)
    {
        _devicePoller = devicePoller;
    }
    
    [HttpPost("Details")]
    public async Task<IActionResult> Details([FromBody] SNMPConnectionInfo connectionInfo)
    {
        IDevice device = await _devicePoller.PollDetails(connectionInfo);
        
        return Ok(new
        {
            device.IpAddress,
            device.DeviceConnection?.Port,
            device.DeviceConnection?.Community,
            device.Name,
            device.Location,
            device.Contact
        });
    }
    
    [HttpPost("Device")]
    public async Task<IActionResult> Device([FromBody] SNMPConnectionInfo connectionInfo)
    {
        IDevice device = await _devicePoller.PollFull(connectionInfo);
        
        return Ok(device);
    }
    
    [HttpPost("Disks")]
    public async Task<IActionResult> Disks([FromBody] SNMPConnectionInfo connectionInfo)
    {
        List<IDisk> disks = await _devicePoller.PollDisks(connectionInfo);
        
        return Ok(disks);
    }
    
    [HttpPost("Memory")]
    public async Task<IActionResult> Memory([FromBody] SNMPConnectionInfo connectionInfo)
    {
        List<IMemory> memory = await _devicePoller.PollMemory(connectionInfo);
        
        return Ok(memory);
    }
    
    [HttpPost("Cpus")]
    public async Task<IActionResult> Cpus([FromBody] SNMPConnectionInfo connectionInfo)
    {
        List<ICpu> cpus = await _devicePoller.PollCpus(connectionInfo);
        
        return Ok(cpus);
    }
    
    [HttpPost("Interfaces")]
    public async Task<IActionResult> Interfaces([FromBody] SNMPConnectionInfo connectionInfo)
    {
        List<IInterface> interfaces = await _devicePoller.PollInterfaces(connectionInfo);
        
        return Ok(interfaces);
    }
}