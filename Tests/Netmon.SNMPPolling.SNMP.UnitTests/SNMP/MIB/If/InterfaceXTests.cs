using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.MIB.If.Interface;
using Netmon.SNMPPolling.SNMP.MIB.If.InterfaceX;
using Netmon.SNMPPolling.SNMP.Result;
using NUnit.Framework;

namespace Netmon.SNMPPolling.SNMP.UnitTests.SNMP.MIB.If;

public class InterfaceXTests
{
    [Test]
    public void IfXTableDeserializer_ShouldReturnIfTable_WhenValidISNMPResult()
    {
        // Arrange
        ISNMPResult iSnmpResult = new SNMPResult(new List<Variable>
        {
            new(new ObjectIdentifier("1.3.6.1.2.1.31.1.1.1.6.0"), new Counter64(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.31.1.1.1.7.0"), new Counter64(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.31.1.1.1.8.0"), new Counter64(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.31.1.1.1.9.0"), new Counter64(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.31.1.1.1.10.0"), new Counter64(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.31.1.1.1.11.0"), new Counter64(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.31.1.1.1.12.0"), new Counter64(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.31.1.1.1.13.0"), new Counter64(0))
        });

        // Act
        IfXTable result = IfXTable.Deserializer.Deserialize(iSnmpResult);

        // Assert
        Assert.That(result.IfXEntries, Has.Count.EqualTo(1));
        Assert.That(result.IfXEntries[0].IfIndex, Is.EqualTo(0));
        Assert.That(result.IfXEntries[0].IfHCInOctets, Is.EqualTo(new Counter64(0)));
        Assert.That(result.IfXEntries[0].IfHCInUcastPkts, Is.EqualTo(new Counter64(0)));
        Assert.That(result.IfXEntries[0].IfHCInMulticastPkts, Is.EqualTo(new Counter64(0)));
        Assert.That(result.IfXEntries[0].IfHCInBroadcastPkts, Is.EqualTo(new Counter64(0)));
        Assert.That(result.IfXEntries[0].IfHCOutOctets, Is.EqualTo(new Counter64(0)));
        Assert.That(result.IfXEntries[0].IfHCOutUcastPkts, Is.EqualTo(new Counter64(0)));
        Assert.That(result.IfXEntries[0].IfHCOutMulticastPkts, Is.EqualTo(new Counter64(0)));
        Assert.That(result.IfXEntries[0].IfHCOutBroadcastPkts, Is.EqualTo(new Counter64(0)));
    }
}