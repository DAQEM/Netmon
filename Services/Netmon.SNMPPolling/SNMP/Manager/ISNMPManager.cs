using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.Manager;

public interface ISNMPManager
{
    Task<ISNMPResult> BulkWalkAsync(SNMPConnectionInfo snmpConnectionInfo, string oid, int timeoutMillis);
}