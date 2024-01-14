namespace Netmon.Models.Exceptions;

public class NetmonException : Exception
{
    protected NetmonException(string? message) : base(message)
    {
    }

    public NetmonException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}