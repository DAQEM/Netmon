using Lextm.SharpSnmpLib;
using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Disk.Metric;
using Netmon.SNMPPolling.SNMP.Converter.Component;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;
using NUnit.Framework;

namespace Netmon.SNMPPolling.Tests.SNMP.Converter.Component;

[TestFixture]
public class MIBDiskConverterTests
{
    [Test]
    public void ConvertMIBsToComponent_ShouldReturnEmptyList_WhenNoHostResourcesMIB()
    {
        // Arrange
        List<IMIB> mibs = new();

        MIBDiskConverter converter = new();

        // Act
        List<IDisk> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.IsEmpty(result);
    }

    [Test]
    public void ConvertMIBsToComponent_ShouldReturnDisksWithMetrics_WhenHostResourcesMIBExists()
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
                        },
                        new()
                        {
                            HrStorageIndex = new Integer32(2),
                            HrStorageType = HrStorageEntry.StorageType.RemovableDisk,
                            HrStorageDescr = new OctetString("D:"),
                            HrStorageAllocationUnits = new Integer32(512),
                            HrStorageSize = new Integer32(5120),
                            HrStorageUsed = new Integer32(2560)
                        }
                    }
                }
            }
        };

        List<IMIB> mibs = new() { hostResourcesMIB };

        MIBDiskConverter converter = new();

        // Act
        List<IDisk> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Has.Count.EqualTo(1));

        IDisk disk = result.First();
        Assert.That(disk.Index, Is.EqualTo(1));
        Assert.That(disk.MountingPoint, Is.EqualTo("C:"));
        Assert.That(disk.Metrics, Is.Not.Empty);
        Assert.That(disk.Metrics, Has.Count.EqualTo(1));

        IDiskMetric metric = disk.Metrics.First();
        Assert.That(metric, Is.InstanceOf<DiskMetric>());
        Assert.That(((DiskMetric)metric).AllocationUnits, Is.EqualTo(1024));
        Assert.That(((DiskMetric)metric).TotalSpace, Is.EqualTo(10240));
        Assert.That(((DiskMetric)metric).UsedSpace, Is.EqualTo(5120));
    }

    [Test]
    public void ConvertMIBsToComponent_ShouldSkipNonFixedDisks_WhenHostResourcesMIBExists()
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
                            HrStorageDescr = new OctetString("RAM Disk"),
                            HrStorageAllocationUnits = new Integer32(1024),
                            HrStorageSize = new Integer32(10240),
                            HrStorageUsed = new Integer32(5120)
                        }
                    }
                }
            }
        };

        List<IMIB> mibs = new() { hostResourcesMIB };

        MIBDiskConverter converter = new();

        // Act
        List<IDisk> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.IsEmpty(result);
    }
}