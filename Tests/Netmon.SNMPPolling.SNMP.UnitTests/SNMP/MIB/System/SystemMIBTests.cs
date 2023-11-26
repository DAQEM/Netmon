using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.Result;
using NUnit.Framework;

namespace Netmon.SNMPPolling.SNMP.UnitTests.SNMP.MIB.System;

public class SystemMIBTests
{
    [Test]
    public void SystemMIB_ShouldReturnSystemMIB_WhenValidISNMPResult()
    {
        // Arrange
        ISNMPResult iSnmpResult = new SNMPResult(new List<Variable>
        {
            new(new ObjectIdentifier("1.3.6.1.2.1.1.1.0"), new OctetString("Description")),
            new(new ObjectIdentifier("1.3.6.1.2.1.1.2.0"), new ObjectIdentifier("1.1")),
            new(new ObjectIdentifier("1.3.6.1.2.1.1.3.0"), new TimeTicks(1)),
            new(new ObjectIdentifier("1.3.6.1.2.1.1.4.0"), new OctetString("Contact")),
            new(new ObjectIdentifier("1.3.6.1.2.1.1.5.0"), new OctetString("Name")),
            new(new ObjectIdentifier("1.3.6.1.2.1.1.6.0"), new OctetString("Location")),
            new(new ObjectIdentifier("1.3.6.1.2.1.1.7.0"), new Integer32(2))
        });

        // Act
        SystemMIB result = SystemMIB.Deserializer.Deserialize(iSnmpResult);

        // Assert
        Assert.That(result.SysDescr, Is.EqualTo(new OctetString("Description")));
        Assert.That(result.SysObjectId, Is.EqualTo(new ObjectIdentifier("1.1")));
        Assert.That(result.SysUpTime, Is.EqualTo(new TimeTicks(1)));
        Assert.That(result.SysContact, Is.EqualTo(new OctetString("Contact")));
        Assert.That(result.SysName, Is.EqualTo(new OctetString("Name")));
        Assert.That(result.SysLocation, Is.EqualTo(new OctetString("Location")));
        Assert.That(result.SysServices, Is.EqualTo(new Integer32(2)));
    }
}