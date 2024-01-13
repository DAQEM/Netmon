using Lextm.SharpSnmpLib;
using Netmon.Models.Device;
using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;
using Netmon.SNMPPolling.SNMP.Converter.Device;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.Request;
using NUnit.Framework;

namespace Netmon.SNMPPolling.SNMP.UnitTests.SNMP.Converter.Device;

[TestFixture]
public class MIBDeviceConverterTests
{
    [Test]
    public void ConvertMIBsToDevice_ShouldReturnDeviceWithConnectionInfo_WhenSystemMIBExists()
    {
        // Arrange
        SNMPConnectionInfo connectionInfo = new()
        {
            IpAddress = "192.168.1.1",
            Port = 161,
            Community = "public",
            AuthPassword = "authPass",
            PrivacyPassword = "privPass",
            AuthProtocol = AuthProtocol.SHA512,
            PrivacyProtocol = PrivacyProtocol.AES256,
            ContextName = "contextName",
            Version = VersionCode.V3
        };

        List<IMIB> mibs = new()
        {
            new SystemMIB
            {
                SysName = new OctetString("Device1"),
                SysLocation = new OctetString("Location1"),
                SysContact = new OctetString("Contact1")
            }
        };

        MIBDeviceConverter converter = new();

        // Act
        IDevice result = converter.ConvertMIBsToDevice(connectionInfo, mibs);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Models.Device.Device>());
        Assert.That(result.IpAddress, Is.EqualTo("192.168.1.1"));

        IDeviceConnection? deviceConnection = result.DeviceConnection;
        Assert.That(deviceConnection, Is.Not.Null);
        Assert.That(deviceConnection?.Port, Is.EqualTo(161));
        Assert.That(deviceConnection?.Community, Is.EqualTo("public"));
        Assert.That(deviceConnection?.AuthPassword, Is.EqualTo("authPass"));
        Assert.That(deviceConnection?.PrivacyPassword, Is.EqualTo("privPass"));
        Assert.That(deviceConnection?.AuthProtocol, Is.EqualTo(AuthProtocol.SHA512));
        Assert.That(deviceConnection?.PrivacyProtocol, Is.EqualTo(PrivacyProtocol.AES256));
        Assert.That(deviceConnection?.ContextName, Is.EqualTo("contextName"));
        Assert.That(deviceConnection?.SNMPVersion, Is.EqualTo(3));

        Assert.That(result.Name, Is.EqualTo("Device1"));
        Assert.That(result.Location, Is.EqualTo("Location1"));
        Assert.That(result.Contact, Is.EqualTo("Contact1"));
    }

    [Test]
    public void ConvertMIBsToDevice_ShouldReturnDeviceWithDefaultConnectionInfo_WhenSystemMIBDoesNotExist()
    {
        // Arrange
        SNMPConnectionInfo connectionInfo = new()
        {
            IpAddress = "192.168.1.1"
        };

        List<IMIB> mibs = new();

        MIBDeviceConverter converter = new();

        // Act
        IDevice result = converter.ConvertMIBsToDevice(connectionInfo, mibs);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Models.Device.Device>());
        Assert.That(result.IpAddress, Is.EqualTo("192.168.1.1"));

        IDeviceConnection? deviceConnection = result.DeviceConnection;
        Assert.That(deviceConnection, Is.Not.Null);
        Assert.That(deviceConnection?.Port, Is.EqualTo(161)); // Default SNMP port
        Assert.That(deviceConnection?.Community, Is.EqualTo("public")); // Default SNMP community
        Assert.That(deviceConnection?.AuthPassword, Is.EqualTo(string.Empty));
        Assert.That(deviceConnection?.PrivacyPassword, Is.EqualTo(string.Empty));
        Assert.That(deviceConnection?.AuthProtocol, Is.EqualTo(AuthProtocol.SHA256));
        Assert.That(deviceConnection?.PrivacyProtocol, Is.EqualTo(PrivacyProtocol.AES));
        Assert.That(deviceConnection?.ContextName, Is.EqualTo(string.Empty));
        Assert.That(deviceConnection?.SNMPVersion, Is.EqualTo(3)); // Default to SNMPv3

        Assert.That(result.Name, Is.EqualTo(null));
        Assert.That(result.Location, Is.EqualTo(null));
        Assert.That(result.Contact, Is.EqualTo(null));
    }
}