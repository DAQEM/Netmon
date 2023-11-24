using System.Net;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Security;
using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;
using Netmon.SNMPPolling.SNMP.Security;

namespace Netmon.SNMPPolling.SNMP.Manager;

public class SNMPManager : ISNMPManager
{
    public async Task<ISNMPResult> BulkWalkAsync(SNMPConnectionInfo snmpConnectionInfo, string oid, int timeoutMillis)
    {
        List<Variable> result = new();
        
        IPEndPoint ipEndPoint = new(IPAddress.Parse(snmpConnectionInfo.IpAddress), snmpConnectionInfo.Port);

        IPrivacyProvider? privacy = GetPrivacyProvider(snmpConnectionInfo);

        Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
        Task<ReportMessage> reportTask = discovery.GetResponseAsync(ipEndPoint);
        
        await Task.WhenAny(reportTask, Task.Delay(timeoutMillis));
        
        if (!reportTask.IsCompletedSuccessfully)
        {
            return new SNMPResult(new List<Variable>());
        }
        
        ReportMessage report = reportTask.Result;

        Task operationTask = Messenger.BulkWalkAsync(
            snmpConnectionInfo.Version,
            ipEndPoint,
            new OctetString(snmpConnectionInfo.Community),
            new OctetString(snmpConnectionInfo.ContextName ?? string.Empty),
            new ObjectIdentifier(oid),
            result,
            10,
            WalkMode.WithinSubtree,
            privacy!,
            report);

        await Task.WhenAny(operationTask, Task.Delay(timeoutMillis));
        
        if (operationTask.IsCompletedSuccessfully)
        {
            return new SNMPResult(result);
        }

        return new SNMPResult(new List<Variable>());
    }

    private static IPrivacyProvider? GetPrivacyProvider(SNMPConnectionInfo snmpConnectionInfo)
    {
        if (snmpConnectionInfo.Version == VersionCode.V3)
        {
            if (snmpConnectionInfo is { AuthProtocol: not null, PrivacyProtocol: not null })
            {
                return PrivacyProvider.GetPrivacyProvider(
                    snmpConnectionInfo.AuthPassword ?? string.Empty,
                    snmpConnectionInfo.PrivacyPassword ?? string.Empty,
                    snmpConnectionInfo.AuthProtocol,
                    snmpConnectionInfo.PrivacyProtocol);
            }
        }

        return null;
    }
}