using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.If;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis;
using Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIBs;

public class MIBsPoller : IMIBsPoller
{
    private readonly ISNMPManager _snmpManager;
    
    private readonly IMIBPoller<SystemMIB> _systemMIBPoller;
    private readonly IMIBPoller<HostResourcesMIB> _hostResourcesMIBPoller;
    private readonly IMIBPoller<IfMIB> _ifMIBPoller;
    private readonly IMIBPoller<UCDavisMIB> _ucDavisMIBPoller;

    public MIBsPoller(ISNMPManager snmpManager, IMIBPoller<SystemMIB> systemMIBPoller, IMIBPoller<HostResourcesMIB> hostResourcesMIBPoller, IMIBPoller<IfMIB> ifMIBPoller, IMIBPoller<UCDavisMIB> ucDavisMIBPoller)
    {
        _snmpManager = snmpManager;
        _systemMIBPoller = systemMIBPoller;
        _hostResourcesMIBPoller = hostResourcesMIBPoller;
        _ifMIBPoller = ifMIBPoller;
        _ucDavisMIBPoller = ucDavisMIBPoller;
    }

    public async Task<List<IMIB>> PollAllMIBs(SNMPConnectionInfo snmpConnectionInfo)
    {
        return new List<IMIB>
        {
            await _systemMIBPoller.PollMIB(snmpConnectionInfo),
            await _hostResourcesMIBPoller.PollMIB(snmpConnectionInfo),
            await _ifMIBPoller.PollMIB(snmpConnectionInfo),
            await _ucDavisMIBPoller.PollMIB(snmpConnectionInfo)
        };
    }

    public async Task<SystemMIB> PollSystemMIB(SNMPConnectionInfo snmpConnectionInfo)
    {
        return await _systemMIBPoller.PollMIB(snmpConnectionInfo);
    }
}