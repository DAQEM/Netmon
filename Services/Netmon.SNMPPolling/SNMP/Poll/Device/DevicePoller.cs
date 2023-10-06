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

public class DevicePoller : IDevicePoller
{
    private readonly IMIBsPoller _mibsPoller;
    
    private readonly IMIBDeviceConverter _deviceConverter;
    private readonly IMIBComponentConverter<IDisk> _disksConverter;
    private readonly IMIBComponentConverter<IMemory> _memoryConverter;
    private readonly IMIBComponentConverter<ICpu> _cpusConverter;
    private readonly IMIBComponentConverter<IInterface> _interfacesConverter;

    public DevicePoller(IMIBsPoller mibsPoller, IMIBDeviceConverter deviceConverter,
        IMIBComponentConverter<IDisk> disksConverter, IMIBComponentConverter<IMemory> memoryConverter, 
        IMIBComponentConverter<ICpu> cpusConverter, IMIBComponentConverter<IInterface> interfacesConverter)
    {
        _mibsPoller = mibsPoller;
        _deviceConverter = deviceConverter;
        _disksConverter = disksConverter;
        _memoryConverter = memoryConverter;
        _cpusConverter = cpusConverter;
        _interfacesConverter = interfacesConverter;
    }

    public async Task<IDevice> PollFull(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await _mibsPoller.PollAllMIBs(connectionInfo);

        IDevice device = _deviceConverter.ConvertMIBsToDevice(connectionInfo, mibs);
        
        device.Disks = _disksConverter.ConvertMIBsToComponent(mibs);
        device.Memory = _memoryConverter.ConvertMIBsToComponent(mibs);
        device.Cpus = _cpusConverter.ConvertMIBsToComponent(mibs);
        device.Interfaces = _interfacesConverter.ConvertMIBsToComponent(mibs);
        
        return device;
    }

    public async Task<IDevice> PollDetails(SNMPConnectionInfo connectionInfo)
    {
        SystemMIB mib = await _mibsPoller.PollSystemMIB(connectionInfo);
        
        return _deviceConverter.ConvertMIBsToDevice(connectionInfo, new List<IMIB> { mib });
    }
    
    public async Task<List<IDisk>> PollDisks(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await _mibsPoller.PollAllMIBs(connectionInfo);

        return _disksConverter.ConvertMIBsToComponent(mibs);
    }
    
    public async Task<List<IMemory>> PollMemory(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await _mibsPoller.PollAllMIBs(connectionInfo);

        return _memoryConverter.ConvertMIBsToComponent(mibs);
    }
    
    public async Task<List<ICpu>> PollCpus(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await _mibsPoller.PollAllMIBs(connectionInfo);

        return _cpusConverter.ConvertMIBsToComponent(mibs);
    }
    
    public async Task<List<IInterface>> PollInterfaces(SNMPConnectionInfo connectionInfo)
    {
        List<IMIB> mibs = await _mibsPoller.PollAllMIBs(connectionInfo);

        return _interfacesConverter.ConvertMIBsToComponent(mibs);
    }
}