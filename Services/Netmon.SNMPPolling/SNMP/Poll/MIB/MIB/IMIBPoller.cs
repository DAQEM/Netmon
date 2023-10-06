using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;

public interface IMIBPoller<T> where T : IMIB
{
    Task<T> PollMIB(SNMPConnectionInfo connectionInfo);
}