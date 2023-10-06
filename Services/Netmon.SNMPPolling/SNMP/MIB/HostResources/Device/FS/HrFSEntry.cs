using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.FS;

public class HrFSEntry
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.8.1";

    public Integer32 HrFSIndex { get; set; }
    public OctetString HrFSMountPoint { get; set; }
    public OctetString HrFSRemoteMountPoint { get; set; }
    public ObjectIdentifier HrFSType { get; set; }
    public Integer32 HrFSAccess { get; set; }
    public Integer32 HrFSBootable { get; set; }
    public Integer32 HrFSStorageIndex { get; set; }
    public OctetString HrFSLastFullBackupDate { get; set; }
    public OctetString HrFSLastPartialBackupDate { get; set; }
    
    public static ISNMPDeserializer<HrFSEntry> Deserializer { get; } = new HrFSEntryDeserializer();
    
    private class HrFSEntryDeserializer : ISNMPDeserializer<HrFSEntry>
    {
        public HrFSEntry Deserialize(ISNMPResult isnmpResult)
        {
            Dictionary<string, Variable> variables = isnmpResult.Variables.ToDictionary(v => v.Id.ToString().Split(".").Reverse().Skip(1).First(), v => v);
            
            return new HrFSEntry
            {
                HrFSIndex = (Integer32) variables["1"].Data,
                HrFSMountPoint = (OctetString) variables["2"].Data,
                HrFSRemoteMountPoint = (OctetString) variables["3"].Data,
                HrFSType = (ObjectIdentifier) variables["4"].Data,
                HrFSAccess = (Integer32) variables["5"].Data,
                HrFSBootable = (Integer32) variables["6"].Data,
                HrFSStorageIndex = (Integer32) variables["7"].Data,
                HrFSLastFullBackupDate = (OctetString) variables["8"].Data,
                HrFSLastPartialBackupDate = (OctetString) variables["9"].Data
            };
        }
    }
}