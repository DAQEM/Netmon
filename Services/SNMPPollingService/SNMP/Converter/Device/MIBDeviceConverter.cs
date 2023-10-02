using DevicesLib.Entities.Device;
using DevicesLib.Protocol;
using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Converter.Device;

public class MIBDeviceConverter : IMIBDeviceConverter
{
    public IDevice ConvertMIBsToDevice(SNMPConnectionInfo connectionInfo, List<IMIB> mibs)
    {
        IDevice device = new DevicesLib.Entities.Device.Device
        {
            IpAddress = connectionInfo.IpAddress,
            Port = connectionInfo.Port,
            Community = connectionInfo.Community,
            AuthPassword = connectionInfo.AuthPassword ?? string.Empty,
            PrivacyPassword = connectionInfo.PrivacyPassword ?? string.Empty,
            AuthProtocol = connectionInfo.AuthProtocol ?? AuthProtocol.SHA256,
            PrivacyProtocol = connectionInfo.PrivacyProtocol ?? PrivacyProtocol.AES,
            ContextName = connectionInfo.ContextName ?? string.Empty,
            SNMPVersion = (int) connectionInfo.Version
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