using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Security;
using Netmon.Models.Device.Connection.Protocol;
using Netmon.SNMPPolling.Exception.SNMP;

namespace Netmon.SNMPPolling.SNMP.Security;

public static class PrivacyProvider
{
    public static IPrivacyProvider GetPrivacyProvider(string authPassword, string privacyPassword, AuthProtocol? authProtocol, PrivacyProtocol? privacyProtocol)
    {
        IAuthenticationProvider auth;
        IPrivacyProvider priv;

        auth = authProtocol switch
        {
            AuthProtocol.SHA256 => new SHA256AuthenticationProvider(new OctetString(authPassword)),
            AuthProtocol.SHA384 => new SHA384AuthenticationProvider(new OctetString(authPassword)),
            AuthProtocol.SHA512 => new SHA512AuthenticationProvider(new OctetString(authPassword)),
            _ => throw new UnknownAuthProtocolException(authProtocol.ToString())
        };

        priv = privacyProtocol switch
        {
            PrivacyProtocol.AES => new AESPrivacyProvider(new OctetString(privacyPassword), auth),
            PrivacyProtocol.AES192 => new AES192PrivacyProvider(new OctetString(privacyPassword), auth),
            PrivacyProtocol.AES256 => new AES256PrivacyProvider(new OctetString(privacyPassword), auth),
            _ => throw new UnknownPrivacyProtocolException(privacyProtocol.ToString())
        };
        
        return priv;
    }
}