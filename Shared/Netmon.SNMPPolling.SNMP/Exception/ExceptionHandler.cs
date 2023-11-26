using Netmon.SNMPPolling.SNMP.Exception.SNMP;

namespace Netmon.SNMPPolling.SNMP.Exception;

public class ExceptionHandler
{
    public static ExceptionResult HandleException(BaseException exception)
    {
        if (exception is SNMPBaseException snmpException)
        {
            return HandleSNMPException(snmpException);
        }
        return new ExceptionResult();
    }
    
    public static ExceptionResult HandleSNMPException(System.Exception snmpException)
    {
        return snmpException switch
        {
            UnknownAuthProtocolException unknownAuthProtocolException => new ExceptionResult(
                unknownAuthProtocolException.Message, 400),
            UnknownPrivacyProtocolException unknownPrivacyProtocolException => new ExceptionResult(
                unknownPrivacyProtocolException.Message, 400),
            _ => new ExceptionResult()
        };
    }
}