using Microsoft.AspNetCore.Mvc;
using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Memory;
using Netmon.Models.Device;
using Netmon.SNMPPolling.DTO;
using Netmon.SNMPPolling.SNMP.Poll.Device;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.Controllers;

[ApiController]
[Route("Discover")]
public class DiscoverController(IDevicePoller devicePoller) : Controller
{
    [HttpPost("Details")]
    public async Task<IActionResult> Details([FromBody] SNMPConnectionDTO snmpConnectionDto)
    {
        SNMPConnectionInfo snmpConnectionInfo = snmpConnectionDto.ToSNMPConnectionInfo();
        IDevice? device = await devicePoller.PollDetails(snmpConnectionInfo);
        
        if (device == null) return NotFound();
        
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
    public async Task<IActionResult> Device([FromBody] SNMPConnectionDTO snmpConnectionDto)
    {
        SNMPConnectionInfo snmpConnectionInfo = snmpConnectionDto.ToSNMPConnectionInfo();
        IDevice? device = await devicePoller.PollFull(snmpConnectionInfo);
        
        return device == null ? NotFound() : Ok(DeviceOverviewDTO.FromDevice(device));
    }
    
    [HttpPost("Disks")]
    public async Task<IActionResult> Disks([FromBody] SNMPConnectionDTO snmpConnectionDto)
    {
        SNMPConnectionInfo snmpConnectionInfo = snmpConnectionDto.ToSNMPConnectionInfo();
        List<IDisk>? disks = await devicePoller.PollDisks(snmpConnectionInfo);

        return disks == null ? NotFound() : Ok(disks.Select(DiskDTO.FromDisk).ToList());
    }
    
    [HttpPost("Memory")]
    public async Task<IActionResult> Memory([FromBody] SNMPConnectionDTO snmpConnectionDto)
    {
        SNMPConnectionInfo snmpConnectionInfo = snmpConnectionDto.ToSNMPConnectionInfo();
        List<IMemory>? memory = await devicePoller.PollMemory(snmpConnectionInfo);
        
        return memory == null ? NotFound() : Ok(memory.Select(MemoryDTO.FromMemory).ToList());
    }
    
    [HttpPost("Cpus")]
    public async Task<IActionResult> Cpus([FromBody] SNMPConnectionDTO snmpConnectionDto)
    {
        SNMPConnectionInfo snmpConnectionInfo = snmpConnectionDto.ToSNMPConnectionInfo();
        List<ICpu>? cpus = await devicePoller.PollCpus(snmpConnectionInfo);
        
        return cpus == null ? NotFound() : Ok(cpus.Select(CpuDTO.FromCpu).ToList());
    }
    
    [HttpPost("Interfaces")]
    public async Task<IActionResult> Interfaces([FromBody] SNMPConnectionDTO snmpConnectionDto)
    {
        SNMPConnectionInfo snmpConnectionInfo = snmpConnectionDto.ToSNMPConnectionInfo();
        List<IInterface>? interfaces = await devicePoller.PollInterfaces(snmpConnectionInfo);
        
        return interfaces == null ? NotFound() : Ok(interfaces.Select(InterfaceDTO.FromInterface).ToList());
    }
}