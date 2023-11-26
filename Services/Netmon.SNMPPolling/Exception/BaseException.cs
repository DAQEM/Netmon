using System.Runtime.Serialization;

namespace Netmon.SNMPPolling.Exception;

public class BaseException : System.Exception
{
    public BaseException(string? message) : base(message)
    {
    }
}