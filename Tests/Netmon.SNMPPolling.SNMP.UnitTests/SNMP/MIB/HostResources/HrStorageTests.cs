using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;
using Netmon.SNMPPolling.SNMP.Result;
using NUnit.Framework;

namespace Netmon.SNMPPolling.SNMP.UnitTests.SNMP.MIB.HostResources;

public class HrStorageTests
{
    [Test]
    public void HrStorageTableDeserializer_ShouldReturnHrStorageTable_WhenValidISNMPResult()
    {
        // Arrange
        ISNMPResult iSnmpResult = new SNMPResult(new List<Variable>
        {
            new(new ObjectIdentifier("1.3.6.1.2.1.25.2.3.1.1.0"), new Integer32(1)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.2.3.1.2.0"), new ObjectIdentifier("1.4")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.2.3.1.3.0"), new OctetString("Physical Memory")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.2.3.1.4.0"), new Integer32(2)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.2.3.1.5.0"), new Integer32(3)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.2.3.1.6.0"), new Integer32(4)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.2.3.1.7.0"), new Integer32(5)),
        });

        // Act
        HrStorage result = HrStorage.Deserializer.Deserialize(iSnmpResult);

        // Assert
        Assert.That(result.HrStorageTable.HrStorageEntries, Has.Count.EqualTo(1));
        Assert.That(result.HrMemorySize, Is.EqualTo(new Integer32(1)));
        Assert.That(result.HrStorageTable.HrStorageEntries[0].HrStorageIndex, Is.EqualTo(new Integer32(1)));
        Assert.That(result.HrStorageTable.HrStorageEntries[0].HrStorageType, Is.EqualTo(HrStorageEntry.StorageType.FixedDisk));
        Assert.That(result.HrStorageTable.HrStorageEntries[0].HrStorageDescr, Is.EqualTo(new OctetString("Physical Memory")));
        Assert.That(result.HrStorageTable.HrStorageEntries[0].HrStorageAllocationUnits, Is.EqualTo(new Integer32(2)));
        Assert.That(result.HrStorageTable.HrStorageEntries[0].HrStorageSize, Is.EqualTo(new Integer32(3)));
        Assert.That(result.HrStorageTable.HrStorageEntries[0].HrStorageUsed, Is.EqualTo(new Integer32(4)));
        Assert.That(result.HrStorageTable.HrStorageEntries[0].HrStorageAllocationFailures, Is.EqualTo(new Integer32(5)));
    }
}