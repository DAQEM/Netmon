using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.MIB.HostResources.Device.Device;
using SNMPPollingService.SNMP.MIB.HostResources.Device.FS;
using SNMPPollingService.SNMP.MIB.HostResources.Device.Network;
using SNMPPollingService.SNMP.MIB.HostResources.Device.Processor;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.HostResources.Device;

public class HrDevice
{
    public static readonly string OID = "1.3.6.1.2.1.25.3";

    public HrDeviceTable HrDeviceTable { get; set; } = new();
    public HrProcessorTable HrProcessorTable { get; set; } = new();
    public HrNetworkTable HrNetworkTable { get; set; } = new();
    public HrFSTable HrFSTable { get; set; } = new();
    
    public static ISNMPDeserializer<HrDevice> Deserializer { get; } = new HrDeviceDeserializer();

    private class HrDeviceDeserializer : ISNMPDeserializer<HrDevice>
    {
        public HrDevice Deserialize(ISNMPResult isnmpResult)
        {
            return new HrDevice
            {
                HrDeviceTable = HrDeviceTable.Deserializer.Deserialize(new SNMPResult(isnmpResult.GetTable(HrDeviceTable.OID))),
                HrProcessorTable = HrProcessorTable.Deserializer.Deserialize(new SNMPResult(isnmpResult.GetTable(HrProcessorTable.OID))),
                HrNetworkTable = HrNetworkTable.Deserializer.Deserialize(new SNMPResult(isnmpResult.GetTable(HrNetworkTable.OID))),
                HrFSTable = HrFSTable.Deserializer.Deserialize(new SNMPResult(isnmpResult.GetTable(HrFSTable.OID)))
            };
        }
    }
}