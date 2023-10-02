using DevicesLib.Entities.Component.Cpu;
using DevicesLib.Entities.Component.Disk;
using DevicesLib.Entities.Component.Interface;
using DevicesLib.Entities.Component.Memory;
using DevicesLib.Entities.Device;
using SNMPPollingService.SNMP.Converter.Component;
using SNMPPollingService.SNMP.Converter.Device;
using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.Poll.MIB.MIBs;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Poll.Device;

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