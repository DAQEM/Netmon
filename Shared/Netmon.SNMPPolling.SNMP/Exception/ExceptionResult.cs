namespace Netmon.SNMPPolling.SNMP.Exception;

public class ExceptionResult(string message, int code)
{
    public string Message { get; private set; } = message;
    public int StatusCode { get; } = code;
    public bool IsSuccess => StatusCode == 200;

    public ExceptionResult() : this(string.Empty, 200)
    {
    }
}