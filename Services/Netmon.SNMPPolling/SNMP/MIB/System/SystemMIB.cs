using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.System;

public class SystemMIB:  IMIB
{
    public static readonly string OID = "1.3.6.1.2.1.1";
    
    public OctetString SysDescr { get; set; } = null!;
    public ObjectIdentifier SysObjectId { get; set; } = null!;
    public TimeTicks SysUpTime { get; set; } = null!;
    public OctetString SysContact { get; set; } = null!;
    public OctetString SysName { get; set; } = null!;
    public OctetString SysLocation { get; set; } = null!;
    public Integer32 SysServices { get; set; } = null!;
    
    public static ISNMPDeserializer<SystemMIB> Deserializer { get; } = new SystemMIBDeserializer();

    private class SystemMIBDeserializer : ISNMPDeserializer<SystemMIB>
    {
        public SystemMIB Deserialize(ISNMPResult snmpResult)
        {
            return new SystemMIB
            {
                SysDescr = (OctetString)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.1.0")).First().Data,
                SysObjectId = (ObjectIdentifier)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.2.0")).First().Data,
                SysUpTime = (TimeTicks)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.3.0")).First().Data,
                SysContact = (OctetString)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.4.0")).First().Data,
                SysName = (OctetString)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.5.0")).First().Data,
                SysLocation = (OctetString)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.6.0")).First().Data,
                SysServices = (Integer32)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.7.0")).First().Data
            };
        }
    }
}