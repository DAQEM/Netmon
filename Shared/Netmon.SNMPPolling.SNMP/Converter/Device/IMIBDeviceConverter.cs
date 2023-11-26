using Netmon.Models.Device;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Converter.Device;

public interface IMIBDeviceConverter
{
    IDevice ConvertMIBsToDevice(SNMPConnectionInfo connectionInfo, List<IMIB> mibs);
}