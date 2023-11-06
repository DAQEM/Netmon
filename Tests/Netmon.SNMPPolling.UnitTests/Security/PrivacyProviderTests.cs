using Lextm.SharpSnmpLib.Security;
using Netmon.Models.Device.Connection.Protocol;
using Netmon.SNMPPolling.Exception.SNMP;
using Netmon.SNMPPolling.SNMP.Security;
using NUnit.Framework;

namespace Netmon.SNMPPolling.Tests.Security;

[TestFixture]
public class PrivacyProviderTests
{
    [Test]
    public void GetPrivacyProvider_ShouldReturnAESPrivacyProvider_WhenAESProtocolIsSelected()
    {
        // Arrange
        string authPassword = "test_auth_password";
        string privacyPassword = "test_privacy_password";
        AuthProtocol authProtocol = AuthProtocol.SHA256;
        PrivacyProtocol privacyProtocol = PrivacyProtocol.AES;

        // Act
        IPrivacyProvider result = PrivacyProvider.GetPrivacyProvider(authPassword, privacyPassword, authProtocol, privacyProtocol);

        // Assert
        Assert.IsInstanceOf<AESPrivacyProvider>(result);
    }

    [Test]
    public void GetPrivacyProvider_ShouldReturnAES192PrivacyProvider_WhenAES192ProtocolIsSelected()
    {
        // Arrange
        string authPassword = "test_auth_password";
        string privacyPassword = "test_privacy_password";
        AuthProtocol authProtocol = AuthProtocol.SHA256;
        PrivacyProtocol privacyProtocol = PrivacyProtocol.AES192;

        // Act
        IPrivacyProvider result = PrivacyProvider.GetPrivacyProvider(authPassword, privacyPassword, authProtocol, privacyProtocol);

        // Assert
        Assert.IsInstanceOf<AES192PrivacyProvider>(result);
    }

    [Test]
    public void GetPrivacyProvider_ShouldThrowUnknownAuthProtocolException_WhenUnknownAuthProtocolIsSelected()
    {
        // Arrange
        string authPassword = "test_auth_password";
        string privacyPassword = "test_privacy_password";
        AuthProtocol authProtocol = (AuthProtocol)100; // An unknown protocol value
        PrivacyProtocol privacyProtocol = PrivacyProtocol.AES;

        // Act and Assert
        Assert.Throws<UnknownAuthProtocolException>(() =>
        {
            PrivacyProvider.GetPrivacyProvider(authPassword, privacyPassword, authProtocol, privacyProtocol);
        });
    }

    [Test]
    public void GetPrivacyProvider_ShouldThrowUnknownPrivacyProtocolException_WhenUnknownPrivacyProtocolIsSelected()
    {
        // Arrange
        string authPassword = "test_auth_password";
        string privacyPassword = "test_privacy_password";
        AuthProtocol authProtocol = AuthProtocol.SHA256;
        PrivacyProtocol privacyProtocol = (PrivacyProtocol)100; // An unknown protocol value

        // Act and Assert
        Assert.Throws<UnknownPrivacyProtocolException>(() =>
        {
            PrivacyProvider.GetPrivacyProvider(authPassword, privacyPassword, authProtocol, privacyProtocol);
        });
    }
}