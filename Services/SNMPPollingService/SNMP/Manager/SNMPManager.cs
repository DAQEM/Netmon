using System.Net;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Security;
using SNMPPollingService.SNMP.Request;
using SNMPPollingService.SNMP.Result;
using SNMPPollingService.SNMP.Security;

namespace SNMPPollingService.SNMP.Manager;

public class SNMPManager : ISNMPManager
{
    public async Task<ISNMPResult> BulkWalkAsync(SNMPConnectionInfo snmpConnectionInfo, string oid)
    {
        List<Variable> result = new();
        
        IPEndPoint ipEndPoint = new(IPAddress.Parse(snmpConnectionInfo.IpAddress), snmpConnectionInfo.Port);


        IPrivacyProvider? privacy = null;

        if (snmpConnectionInfo.Version == VersionCode.V3)
        {
            if (snmpConnectionInfo is { AuthProtocol: not null, PrivacyProtocol: not null })
            {
                privacy = PrivacyProvider.GetPrivacyProvider(
                    snmpConnectionInfo.AuthPassword ?? string.Empty,
                    snmpConnectionInfo.PrivacyPassword ?? string.Empty,
                    snmpConnectionInfo.AuthProtocol,
                    snmpConnectionInfo.PrivacyProtocol);
            }
        }
        
        Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
        ReportMessage report = await discovery.GetResponseAsync(ipEndPoint);
        
        await Messenger.BulkWalkAsync(snmpConnectionInfo.Version,
            ipEndPoint,
            new OctetString(snmpConnectionInfo.Community),
            new OctetString(snmpConnectionInfo.ContextName ?? string.Empty),
            new ObjectIdentifier(oid),
            result,
            10,
            WalkMode.WithinSubtree,
            privacy!,
            report);
        
        return new SNMPResult(result);
    }
}