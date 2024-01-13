using System.Net.NetworkInformation;
using Lextm.SharpSnmpLib;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Interface.Metric;
using Netmon.SNMPPolling.SNMP.Converter.Component;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.If;
using Netmon.SNMPPolling.SNMP.MIB.If.Interface;
using Netmon.SNMPPolling.SNMP.MIB.If.InterfaceX;
using NUnit.Framework;

namespace Netmon.SNMPPolling.SNMP.UnitTests.SNMP.Converter.Component;

[TestFixture]
public class MIBInterfaceConverterTests
{
    [Test]
    public void ConvertMIBsToComponent_ShouldReturnEmptyList_WhenNoIfMIB()
    {
        // Arrange
        List<IMIB> mibs = new();

        MIBInterfaceConverter converter = new();

        // Act
        List<IInterface> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ConvertMIBsToComponent_ShouldReturnInterfacesWithMetrics_WhenIfMIBExists()
    {
        // Arrange
        IfMIB ifMIB = new()
        {
            IfTable = new IfTable
            {
                IfEntries = new List<IfEntry>
                {
                    new()
                    {
                        IfIndex = new Integer32(1),
                        IfDescr = new OctetString("eth0"),
                        IfType = InterfaceType.EthernetCsmacd,
                        IfPhysAddress = new OctetString(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB }),
                        IfAdminStatus = InterfaceStatus.Up,
                        IfOperationalStatus = InterfaceStatus.Up,
                        IfSpeed = new Gauge32(1000000000), // 1 Gbps
                        IfMtu = new Integer32(1500),
                        IfInErrors = new Counter32(10),
                        IfOutErrors = new Counter32(5),
                        IfInDiscards = new Counter32(2),
                        IfOutDiscards = new Counter32(1)
                    }
                }
            },
            IfXTable = new IfXTable
            {
                IfXEntries = new List<IfXEntry>
                {
                    new()
                    {
                        IfIndex = 1,
                        IfHCInOctets = new Counter64(1000),
                        IfHCOutOctets = new Counter64(500),
                        IfHCInBroadcastPkts = new Counter64(100),
                        IfHCOutBroadcastPkts = new Counter64(50),
                        IfHCInMulticastPkts = new Counter64(50),
                        IfHCOutMulticastPkts = new Counter64(25),
                        IfHCInUcastPkts = new Counter64(900),
                        IfHCOutUcastPkts = new Counter64(450)
                    }
                }
            }
        };

        List<IMIB> mibs = new() { ifMIB };

        MIBInterfaceConverter converter = new();

        // Act
        List<IInterface> result = converter.ConvertMIBsToComponent(mibs);

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Has.Count.EqualTo(1));

        IInterface iface = result.First();
        Assert.That(iface.Index, Is.EqualTo(1));
        Assert.That(iface.Name, Is.EqualTo("eth0"));
        Assert.That(iface.Type, Is.EqualTo(InterfaceType.EthernetCsmacd));
        Assert.That(iface.PhysAddress, Is.EqualTo(PhysicalAddress.Parse("01-23-45-67-89-AB")));
        Assert.That(iface.Metrics, Is.Not.Empty);
        Assert.That(iface.Metrics, Has.Count.EqualTo(1));

        IInterfaceMetric metric = iface.Metrics.First();
        Assert.That(metric, Is.InstanceOf<InterfaceMetric>());
        Assert.That(((InterfaceMetric)metric).AdminStatus, Is.EqualTo(InterfaceStatus.Up));
        Assert.That(((InterfaceMetric)metric).OperationStatus, Is.EqualTo(InterfaceStatus.Up));
        Assert.That(((InterfaceMetric)metric).Speed, Is.EqualTo(1000000000));
        Assert.That(((InterfaceMetric)metric).Mtu, Is.EqualTo(1500));
        Assert.That(((InterfaceMetric)metric).InOctets, Is.EqualTo(1000));
        Assert.That(((InterfaceMetric)metric).OutOctets, Is.EqualTo(500));
        Assert.That(((InterfaceMetric)metric).InErrors, Is.EqualTo(10));
        Assert.That(((InterfaceMetric)metric).OutErrors, Is.EqualTo(5));
        Assert.That(((InterfaceMetric)metric).InBroadcastPackets, Is.EqualTo(100));
        Assert.That(((InterfaceMetric)metric).OutBroadcastPackets, Is.EqualTo(50));
        Assert.That(((InterfaceMetric)metric).InMulticastPackets, Is.EqualTo(50));
        Assert.That(((InterfaceMetric)metric).OutMulticastPackets, Is.EqualTo(25));
        Assert.That(((InterfaceMetric)metric).InUnicastPackets, Is.EqualTo(900));
        Assert.That(((InterfaceMetric)metric).OutUnicastPackets, Is.EqualTo(450));
    }
}