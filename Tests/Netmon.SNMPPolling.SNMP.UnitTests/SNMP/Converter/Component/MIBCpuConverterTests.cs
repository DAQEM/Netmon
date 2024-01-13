using Lextm.SharpSnmpLib;
using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;
using Netmon.Models.Component.Cpu.Metric;
using Netmon.SNMPPolling.SNMP.Converter.Component;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Device;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.Device;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.Processor;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis.CpuLoad;
using NUnit.Framework;

namespace Netmon.SNMPPolling.SNMP.UnitTests.SNMP.Converter.Component;

[TestFixture]
public class MIBCpuConverterTests
{
    [Test]
    public void ConvertMIBsToComponent_ShouldReturnEmptyList_WhenNoHostResourcesMIB()
    {
        // Arrange
        List<IMIB> mibs = new();
        MIBCpuConverter converter = new();

        // Act
        List<ICpu> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ConvertMIBsToComponent_ShouldReturnCpuWithCoresAndMetrics_WhenHostResourcesMIBExists()
    {
        // Arrange
        HostResourcesMIB hostResourcesMIB = new()
        {
            HrDevice = new HrDevice
            {
                HrProcessorTable = new HrProcessorTable
                {
                    HrProcessorEntries = new List<HrProcessorEntry>
                    {
                        new()
                        {
                            HrProcessorIndex = new Integer32(1),
                            HrProcessorLoad = new Integer32(50)
                        }
                    }
                },
                HrDeviceTable = new HrDeviceTable
                {
                    HrDeviceEntries = new List<HrDeviceEntry>
                    {
                        new()
                        {
                            HrDeviceIndex = new Integer32(1),
                            HrDeviceDescr = new OctetString("Processor 1")
                        }
                    }
                }
            }
        };

        List<IMIB> mibs = new() { hostResourcesMIB };

        MIBCpuConverter converter = new();

        // Act
        List<ICpu> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Has.Count.EqualTo(1));

        ICpu cpu = result.First();
        Assert.That(cpu.Index, Is.EqualTo(1));
        Assert.That(cpu.Cores, Is.Not.Empty);
        Assert.That(cpu.Cores, Has.Count.EqualTo(1));

        ICpuCore core = cpu.Cores.First();
        Assert.That(core.Index, Is.EqualTo(1));
        Assert.That(core.Name, Is.EqualTo("Processor 1"));
        Assert.That(core.Metrics, Is.Not.Empty);
        Assert.That(core.Metrics, Has.Count.EqualTo(1));

        ICpuCoreMetric metric = core.Metrics.First();
        Assert.That(metric, Is.InstanceOf<CpuCoreMetric>());
        Assert.That(((CpuCoreMetric)metric).Load, Is.EqualTo(50));
    }

    [Test]
    public void ConvertMIBsToComponent_ShouldReturnCpuWithMetrics_WhenUCDavisMIBExists()
    {
        // Arrange
        UCDavisMIB ucDavisMIB = new()
        {
            LaLoadTable = new LaLoadTable
            {
                LaLoadEntries = new List<LaLoadEntry>
                {
                    new() { LaLoadInt = new Integer32(1) },
                    new() { LaLoadInt = new Integer32(2) },
                    new() { LaLoadInt = new Integer32(3) }
                }
            }
        };

        List<IMIB> mibs = new() { ucDavisMIB };

        MIBCpuConverter converter = new();

        // Act
        List<ICpu> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Has.Count.EqualTo(1));

        ICpu cpu = result.First();
        Assert.That(cpu.Index, Is.EqualTo(1));
        Assert.That(cpu.Metrics, Is.Not.Empty);

        ICpuMetric metric = cpu.Metrics.First();
        Assert.That(metric, Is.InstanceOf<CpuMetric>());
        Assert.That(((CpuMetric)metric).OneMinuteLoad, Is.EqualTo(1));
        Assert.That(((CpuMetric)metric).FiveMinuteLoad, Is.EqualTo(2));
        Assert.That(((CpuMetric)metric).FifteenMinuteLoad, Is.EqualTo(3));
    }
}