using DevicesLib.Entities.Device;
using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Converter.Device;

public interface IMIBDeviceConverter
{
    IDevice ConvertMIBsToDevice(SNMPConnectionInfo connectionInfo, List<IMIB> mibs);
}