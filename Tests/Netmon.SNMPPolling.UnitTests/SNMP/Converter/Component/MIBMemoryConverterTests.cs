using Lextm.SharpSnmpLib;
using Netmon.Models.Component.Memory;
using Netmon.Models.Component.Memory.Metric;
using Netmon.SNMPPolling.SNMP.Converter.Component;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;
using NUnit.Framework;

namespace Netmon.SNMPPolling.Tests.SNMP.Converter.Component;

[TestFixture]
public class MIBMemoryConverterTests
{
    [Test]
    public void ConvertMIBsToComponent_ShouldReturnEmptyList_WhenNoHostResourcesMIB()
    {
        // Arrange
        List<IMIB> mibs = new();

        MIBMemoryConverter converter = new();

        // Act
        List<IMemory> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ConvertMIBsToComponent_ShouldReturnMemoryWithMetrics_WhenHostResourcesMIBExists()
    {
        // Arrange
        HostResourcesMIB hostResourcesMIB = new()
        {
            HrStorage = new HrStorage
            {
                HrStorageTable = new HrStorageTable
                {
                    HrStorageEntries = new List<HrStorageEntry>
                    {
                        new()
                        {
                            HrStorageIndex = new Integer32(1),
                            HrStorageType = HrStorageEntry.StorageType.Ram,
                            HrStorageDescr = new OctetString("Physical Memory"),
                            HrStorageAllocationUnits = new Integer32(4096),
                            HrStorageSize = new Integer32(16384),
                            HrStorageUsed = new Integer32(8192)
                        }
                    }
                }
            }
        };

        List<IMIB> mibs = new() { hostResourcesMIB };

        MIBMemoryConverter converter = new();

        // Act
        List<IMemory> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Has.Count.EqualTo(1));

        IMemory memory = result.First();
        Assert.That(memory.Index, Is.EqualTo(1));
        Assert.That(memory.Name, Is.EqualTo("Physical Memory"));
        Assert.That(memory.Metrics, Is.Not.Empty);
        Assert.That(memory.Metrics, Has.Count.EqualTo(1));

        IMemoryMetric metric = memory.Metrics.First();
        Assert.That(metric, Is.InstanceOf<MemoryMetric>());
        Assert.That(((MemoryMetric)metric).AllocationUnits, Is.EqualTo(4096));
        Assert.That(((MemoryMetric)metric).TotalSpace, Is.EqualTo(16384));
        Assert.That(((MemoryMetric)metric).UsedSpace, Is.EqualTo(8192));
    }

    [Test]
    public void ConvertMIBsToComponent_ShouldSkipNonRamStorage_WhenHostResourcesMIBExists()
    {
        // Arrange
        HostResourcesMIB hostResourcesMIB = new()
        {
            HrStorage = new HrStorage
            {
                HrStorageTable = new HrStorageTable
                {
                    HrStorageEntries = new List<HrStorageEntry>
                    {
                        new()
                        {
                            HrStorageIndex = new Integer32(1),
                            HrStorageType = HrStorageEntry.StorageType.FixedDisk,
                            HrStorageDescr = new OctetString("C:"),
                            HrStorageAllocationUnits = new Integer32(1024),
                            HrStorageSize = new Integer32(10240),
                            HrStorageUsed = new Integer32(5120)
                        }
                    }
                }
            }
        };

        List<IMIB> mibs = new() { hostResourcesMIB };

        MIBMemoryConverter converter = new();

        // Act
        List<IMemory> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Empty);
    }
}