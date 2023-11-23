using System.Runtime.Serialization;

namespace Netmon.Models.Exceptions;

public class NetmonException : Exception
{
    public NetmonException()
    {
    }

    protected NetmonException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NetmonException(string? message) : base(message)
    {
    }

    public NetmonException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}