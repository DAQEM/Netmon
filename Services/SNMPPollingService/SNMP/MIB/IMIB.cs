using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.MIB;

public interface IMIB<T> where T : IMIB<T>
{
    Task<T> Poll(ISNMPManager snmpManager, SNMPConnectionInfo connectionInfo);
}