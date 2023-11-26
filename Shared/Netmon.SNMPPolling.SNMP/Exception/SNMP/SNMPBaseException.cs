namespace Netmon.SNMPPolling.SNMP.Exception.SNMP;

public abstract class SNMPBaseException : BaseException
{
    protected SNMPBaseException(string? message) : base(message)
    {
    }
}