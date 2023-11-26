using Lextm.SharpSnmpLib;
using Netmon.Models.Component.Interface;
using Netmon.SNMPPolling.SNMP.MIB.If.Interface;
using Netmon.SNMPPolling.SNMP.Result;
using NUnit.Framework;

namespace Netmon.SNMPPolling.SNMP.UnitTests.SNMP.MIB.If;

public class InterfaceTests
{
    [Test]
    public void IfTableDeserializer_ShouldReturnIfTable_WhenValidISNMPResult()
    {
        // Arrange
        ISNMPResult iSnmpResult = new SNMPResult(new List<Variable>
        {
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.1.0"), new Integer32(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.0"), new OctetString("eth0")),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.3.0"), new Integer32(6)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.4.0"), new Integer32(1500)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.5.0"), new Gauge32(100000000)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.6.0"),
                new OctetString(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.7.0"), new Integer32(1)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.8.0"), new Integer32(1)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.13.0"), new Counter32(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.14.0"), new Counter32(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.19.0"), new Counter32(0)),
            new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.20.0"), new Counter32(0))
        });

        // Act
        IfTable result = IfTable.Deserializer.Deserialize(iSnmpResult);

        // Assert
        Assert.That(result.IfEntries, Has.Count.EqualTo(1));
        Assert.That(result.IfEntries[0].IfIndex, Is.EqualTo(new Integer32(0)));
        Assert.That(result.IfEntries[0].IfDescr, Is.EqualTo(new OctetString("eth0")));
        Assert.That(result.IfEntries[0].IfType, Is.EqualTo(InterfaceType.EthernetCsmacd));
        Assert.That(result.IfEntries[0].IfMtu, Is.EqualTo(new Integer32(1500)));
        Assert.That(result.IfEntries[0].IfSpeed, Is.EqualTo(new Gauge32(100000000)));
        Assert.That(result.IfEntries[0].IfPhysAddress, Is.EqualTo(new OctetString(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })));
        Assert.That(result.IfEntries[0].IfAdminStatus, Is.EqualTo(InterfaceStatus.Up));
        Assert.That(result.IfEntries[0].IfOperationalStatus, Is.EqualTo(InterfaceStatus.Up));
        Assert.That(result.IfEntries[0].IfInDiscards, Is.EqualTo(new Counter32(0)));
        Assert.That(result.IfEntries[0].IfInErrors, Is.EqualTo(new Counter32(0)));
        Assert.That(result.IfEntries[0].IfOutDiscards, Is.EqualTo(new Counter32(0)));
        Assert.That(result.IfEntries[0].IfOutErrors, Is.EqualTo(new Counter32(0)));
    }
}