using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Poll.MIB.MIB;

public interface IMIBPoller<T> where T : IMIB
{
    Task<T> PollMIB(SNMPConnectionInfo connectionInfo);
}