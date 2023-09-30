using Lextm.SharpSnmpLib;
using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.Request;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.System;

public class SystemMIB:  IMIB
{
    public static readonly string OID = "1.3.6.1.2.1.1";
    
    public OctetString SysDescr { get; set; }
    public ObjectIdentifier SysObjectID { get; set; }
    public TimeTicks SysUpTime { get; set; }
    public OctetString SysContact { get; set; }
    public OctetString SysName { get; set; }
    public OctetString SysLocation { get; set; }
    public Integer32 SysServices { get; set; }
    
    public static ISNMPDeserializer<SystemMIB> Deserializer { get; } = new SystemMIBDeserializer();

    private class SystemMIBDeserializer : ISNMPDeserializer<SystemMIB>
    {
        public SystemMIB Deserialize(ISNMPResult snmpResult)
        {
            return new SystemMIB
            {
                SysDescr = (OctetString)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.1.0")).First().Data,
                SysObjectID = (ObjectIdentifier)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.2.0")).First().Data,
                SysUpTime = (TimeTicks)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.3.0")).First().Data,
                SysContact = (OctetString)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.4.0")).First().Data,
                SysName = (OctetString)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.5.0")).First().Data,
                SysLocation = (OctetString)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.6.0")).First().Data,
                SysServices = (Integer32)snmpResult.Variables.Where(v => v.Id.ToString().Equals("1.3.6.1.2.1.1.7.0")).First().Data
            };
        }
    }
}