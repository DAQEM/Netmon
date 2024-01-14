using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Memory;
using Netmon.Models.Device;
using Netmon.SNMPPolling.SNMP.Converter.Component;
using Netmon.SNMPPolling.SNMP.Converter.Device;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.Poll.MIB.MIBs;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Poll.Device;

public class DevicePoller(
    IMIBsPoller mibsPoller,
    IMIBDeviceConverter deviceConverter,
    IMIBComponentConverter<IDisk> disksConverter,
    IMIBComponentConverter<IMemory> memoryConverter,
    IMIBComponentConverter<ICpu> cpusConverter,
    IMIBComponentConverter<IInterface> interfacesConverter)
    : IDevicePoller
{
    public async Task<IDevice?> PollFull(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await mibsPoller.PollAllMIBs(connectionInfo);

        if (!mibs.Any()) return null;
        
        IDevice device = deviceConverter.ConvertMIBsToDevice(connectionInfo, mibs);
        
        device.Disks = disksConverter.ConvertMIBsToComponent(mibs);
        device.Memory = memoryConverter.ConvertMIBsToComponent(mibs);
        device.Cpus = cpusConverter.ConvertMIBsToComponent(mibs);
        device.Interfaces = interfacesConverter.ConvertMIBsToComponent(mibs);
        
        return device;
    }

    public async Task<IDevice?> PollDetails(SNMPConnectionInfo connectionInfo)
    {
        SystemMIB? mib = await mibsPoller.PollSystemMIB(connectionInfo);
        
        if (mib is null) return null;
        
        return deviceConverter.ConvertMIBsToDevice(connectionInfo, new List<IMIB> { mib });
    }
    
    public async Task<List<IDisk>?> PollDisks(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await mibsPoller.PollAllMIBs(connectionInfo);

        if (!mibs.Any()) return null;
        
        return disksConverter.ConvertMIBsToComponent(mibs);
    }
    
    public async Task<List<IMemory>?> PollMemory(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await mibsPoller.PollAllMIBs(connectionInfo);

        if (!mibs.Any()) return null;
        
        return memoryConverter.ConvertMIBsToComponent(mibs);
    }
    
    public async Task<List<ICpu>?> PollCpus(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await mibsPoller.PollAllMIBs(connectionInfo);

        if (!mibs.Any()) return null;
        
        return cpusConverter.ConvertMIBsToComponent(mibs);
    }
    
    public async Task<List<IInterface>?> PollInterfaces(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await mibsPoller.PollAllMIBs(connectionInfo);

        if (!mibs.Any()) return null;
        
        return interfacesConverter.ConvertMIBsToComponent(mibs);
    }
}