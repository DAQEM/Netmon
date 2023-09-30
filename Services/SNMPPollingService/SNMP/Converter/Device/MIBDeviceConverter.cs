using SNMPPollingService.Entities.Device;
using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Converter.Device;

public class MIBDeviceConverter : IMIBDeviceConverter
{
    public IDevice ConvertMIBsToDevice(SNMPConnectionInfo connectionInfo, List<IMIB> mibs)
    {
        IDevice device = new Entities.Device.Device
        {
            IpAddress = connectionInfo.IpAddress,
            Port = connectionInfo.Port,
            Community = connectionInfo.Community
        };

        SystemMIB? systemMIB = mibs.OfType<SystemMIB>().FirstOrDefault();
        if (systemMIB == null)
        {
            return device;
        }

        device.Name = systemMIB.SysName.ToString();
        device.Location = systemMIB.SysLocation.ToString();
        device.Contact = systemMIB.SysContact.ToString();
        
        return device;
    }
}