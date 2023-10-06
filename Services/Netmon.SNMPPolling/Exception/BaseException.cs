using System.Runtime.Serialization;

namespace Netmon.SNMPPolling.Exception;

public class BaseException : System.Exception
{
    protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public BaseException(string? message) : base(message)
    {
    }

    public BaseException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}