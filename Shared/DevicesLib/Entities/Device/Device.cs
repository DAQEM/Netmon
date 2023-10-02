using DevicesLib.DBO.Device;
using DevicesLib.Entities.Component.Cpu;
using DevicesLib.Entities.Component.Disk;
using DevicesLib.Entities.Component.Interface;
using DevicesLib.Entities.Component.Memory;
using DevicesLib.Protocol;

namespace DevicesLib.Entities.Device;

public class Device : IDevice
{
    public string IpAddress { get; set; }
    public int Port { get; set; }
    public string Community { get; set; }
    public string AuthPassword { get; set; }
    public string PrivacyPassword { get; set; }
    public AuthProtocol AuthProtocol { get; set; }
    public PrivacyProtocol PrivacyProtocol { get; set; }
    public string ContextName { get; set; } = "";
    public int SNMPVersion { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Contact { get; set; }
    public List<IDisk> Disks { get; set; }
    public List<ICpu> Cpus { get; set; }
    public List<IMemory> Memory { get; set; }
    public List<IInterface> Interfaces { get; set; }
    
    public DeviceDBO ToDBO()
    {
        return new DeviceDBO
        {
            IpAddress = IpAddress,
            DeviceConnection = new DeviceConnectionDBO
            {
                Id = Guid.NewGuid(),
                Port = Port,
                Community = Community,
                AuthPassword = AuthPassword,
                PrivacyPassword = PrivacyPassword,
                AuthProtocol = AuthProtocol,
                PrivacyProtocol = PrivacyProtocol,
                ContextName = ContextName,
                SNMPVersion = SNMPVersion
            },
            Name = Name ?? "Unknown",
            Location = Location,
            Contact = Contact,
            Disks = Disks.Select(disk => disk.ToDBO()).ToList(),
            Cpus = Cpus.Select(cpu => cpu.ToDBO()).ToList(),
            Memory = Memory.Select(memory => memory.ToDBO()).ToList(),
            Interfaces = Interfaces.Select(inter => inter.ToDBO()).ToList()
        };
    }
}