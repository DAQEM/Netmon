using SNMPPollingService.Entities.Component.Cpu;
using SNMPPollingService.Entities.Component.Disk;
using SNMPPollingService.Entities.Component.Interface;
using SNMPPollingService.Entities.Component.Memory;
using SNMPPollingService.Entities.Device;
using SNMPPollingService.SNMP.Converter.Component;
using SNMPPollingService.SNMP.Converter.Device;
using SNMPPollingService.SNMP.Manager;
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
}