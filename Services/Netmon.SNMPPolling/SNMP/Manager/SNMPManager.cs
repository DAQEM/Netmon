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
        ReportMessage report = await discovery.GetResponseAsync(ipEndPoint);

        using (CancellationTokenSource cts = new())
        {
            cts.CancelAfter(timeoutMillis);

            try
            {
                await Messenger.BulkWalkAsync(snmpConnectionInfo.Version,
                    ipEndPoint,
                    new OctetString(snmpConnectionInfo.Community),
                    new OctetString(snmpConnectionInfo.ContextName ?? string.Empty),
                    new ObjectIdentifier(oid),
                    result,
                    10,
                    WalkMode.WithinSubtree,
                    privacy!,
                    report,
                    cts.Token);
            }
            catch (OperationCanceledException)
            {
                return new SNMPResult(new List<Variable>());
            }
        }

        return new SNMPResult(result);
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