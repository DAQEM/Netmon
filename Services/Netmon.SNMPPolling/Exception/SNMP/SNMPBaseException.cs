namespace Netmon.SNMPPolling.Exception.SNMP;

public abstract class SNMPBaseException : BaseException
{
    protected SNMPBaseException(string? message) : base(message)
    {
    }
}