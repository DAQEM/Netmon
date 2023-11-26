using Netmon.Models.Device;
using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Converter.Device;

public class MIBDeviceConverter : IMIBDeviceConverter
{
    public IDevice ConvertMIBsToDevice(SNMPConnectionInfo connectionInfo, List<IMIB> mibs)
    {
        IDevice device = new Models.Device.Device
        {
            IpAddress = connectionInfo.IpAddress,
            DeviceConnection = new DeviceConnection
            {
                Port = connectionInfo.Port,
                Community = connectionInfo.Community,
                AuthPassword = connectionInfo.AuthPassword ?? string.Empty,
                PrivacyPassword = connectionInfo.PrivacyPassword ?? string.Empty,
                AuthProtocol = connectionInfo.AuthProtocol ?? AuthProtocol.SHA256,
                PrivacyProtocol = connectionInfo.PrivacyProtocol ?? PrivacyProtocol.AES,
                ContextName = connectionInfo.ContextName ?? string.Empty,
                SNMPVersion = (int) connectionInfo.Version
            }
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