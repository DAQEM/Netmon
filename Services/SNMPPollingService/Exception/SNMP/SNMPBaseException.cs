namespace SNMPPollingService.Exception.SNMP;

public abstract class SNMPBaseException : BaseException
{
    protected SNMPBaseException(string? message) : base(message)
    {
    }
}