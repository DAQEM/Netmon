using SNMPPollingService.SNMP.Request;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.Manager;

public interface ISNMPManager
{
    Task<ISNMPResult> BulkWalkAsync(SNMPConnectionInfo snmpConnectionInfo, string oid);
}