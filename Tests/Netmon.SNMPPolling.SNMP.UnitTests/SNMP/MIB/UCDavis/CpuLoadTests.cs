using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis.CpuLoad;
using Netmon.SNMPPolling.SNMP.Result;
using NUnit.Framework;

namespace Netmon.SNMPPolling.SNMP.UnitTests.SNMP.MIB.UCDavis;

public class CpuLoadTests
{
    [Test]
    public void CpuLoadTableDeserializer_ShouldReturnCpuLoadTable_WhenValidISNMPResult()
    {
        // Arrange
        ISNMPResult iSnmpResult = new SNMPResult(new List<Variable>
        {
            new(new ObjectIdentifier("1.3.6.1.4.1.2021.10.1.5.0"), new Integer32(2)),
        });

        // Act
        LaLoadTable result = LaLoadTable.Deserializer.Deserialize(iSnmpResult);

        // Assert
        Assert.That(result.LaLoadEntries, Has.Count.EqualTo(1));
        Assert.That(result.LaLoadEntries[0].LaLoadInt, Is.EqualTo(new Integer32(2)));
    }
}