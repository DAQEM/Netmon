using DevicesLib.Database;
using DevicesLib.DBO.Device;
using DevicesLib.Entities.Component.Cpu;
using DevicesLib.Entities.Component.Disk;
using DevicesLib.Entities.Component.Interface;
using DevicesLib.Entities.Component.Memory;
using DevicesLib.Entities.Device;
using DevicesLib.Repositories.Device;
using Microsoft.AspNetCore.Mvc;
using SNMPPollingService.SNMP.Poll.Device;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.Controllers;

[ApiController]
[Route("Discover")]
public class DiscoverController : Controller
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IDevicePoller _devicePoller;
    
    public DiscoverController(IDeviceRepository deviceRepository, IDevicePoller devicePoller)
    {
        _deviceRepository = deviceRepository;
        _devicePoller = devicePoller;
    }
    
    [HttpPost("Details")]
    public async Task<IActionResult> Details([FromBody] SNMPConnectionInfo connectionInfo)
    {
        IDevice device = await _devicePoller.PollDetails(connectionInfo);
        
        return Ok(new
        {
            device.IpAddress,
            device.Port,
            device.Community,
            device.Name,
            device.Location,
            device.Contact
        });
    }
    
    [HttpPost("Device")]
    public async Task<IActionResult> Device([FromBody] SNMPConnectionInfo connectionInfo)
    {
        IDevice device = await _devicePoller.PollFull(connectionInfo);

        await _deviceRepository.AddOrUpdateFullDevice(device.ToDBO());
        await _deviceRepository.SaveChanges();
        
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