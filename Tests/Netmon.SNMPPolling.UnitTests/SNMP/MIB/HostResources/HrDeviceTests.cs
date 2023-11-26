using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Device;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.Device;
using Netmon.SNMPPolling.SNMP.Result;
using NUnit.Framework;

namespace Netmon.SNMPPolling.Tests.SNMP.MIB.HostResources;

public class HrDeviceTests
{
    [Test]
    public void HrDeviceDeserializer_ShouldReturnHrDevice_WhenValidISNMPResult()
    {
        // Arrange
        ISNMPResult iSnmpResult = new SNMPResult(new List<Variable>
        {
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.1.0"), new Integer32(1)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.2.0"), new ObjectIdentifier("1.1")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.3.0"), new OctetString("Name")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.4.0"), new ObjectIdentifier("1.2")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.5.0"), new Integer32(2)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.6.0"), new Counter32(3)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.3.1.1.0"), new ObjectIdentifier("1.3")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.3.1.2.0"), new Integer32(4)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.4.1.1.0"), new Integer32(5)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.1.0"), new Integer32(6)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.2.0"), new OctetString("MountingPoint")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.3.0"), new OctetString("RemoteMountingPoint")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.4.0"), new ObjectIdentifier("1.4")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.5.0"), new Integer32(7)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.6.0"), new Integer32(8)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.7.0"), new Integer32(9)),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.8.0"), new OctetString("LastFullBackupDate")),
            new(new ObjectIdentifier("1.3.6.1.2.1.25.3.8.1.9.0"), new OctetString("LastPartialBackupDate"))
        });

        // Act
        HrDevice result = HrDevice.Deserializer.Deserialize(iSnmpResult);

        // Assert
        Assert.That(result.HrDeviceTable.HrDeviceEntries, Has.Count.EqualTo(1));
        Assert.That(result.HrDeviceTable.HrDeviceEntries[0].HrDeviceIndex, Is.EqualTo(new Integer32(1)));
        Assert.That(result.HrDeviceTable.HrDeviceEntries[0].HrDeviceType, Is.EqualTo(new ObjectIdentifier("1.1")));
        Assert.That(result.HrDeviceTable.HrDeviceEntries[0].HrDeviceDescr, Is.EqualTo(new OctetString("Name")));
        Assert.That(result.HrDeviceTable.HrDeviceEntries[0].HrDeviceID, Is.EqualTo(new ObjectIdentifier("1.2")));
        Assert.That(result.HrDeviceTable.HrDeviceEntries[0].HrDeviceStatus, Is.EqualTo(new Integer32(2)));
        Assert.That(result.HrDeviceTable.HrDeviceEntries[0].HrDeviceErrors, Is.EqualTo(new Counter32(3)));
        Assert.That(result.HrProcessorTable.HrProcessorEntries, Has.Count.EqualTo(1));
        Assert.That(result.HrProcessorTable.HrProcessorEntries[0].HrProcessorFrwID, Is.EqualTo(new ObjectIdentifier("1.3")));
        Assert.That(result.HrProcessorTable.HrProcessorEntries[0].HrProcessorLoad, Is.EqualTo(new Integer32(4)));
        Assert.That(result.HrNetworkTable.HrNetworkEntries, Has.Count.EqualTo(1));
        Assert.That(result.HrNetworkTable.HrNetworkEntries[0].HrNetworkIfIndex, Is.EqualTo(new Integer32(5)));
        Assert.That(result.HrFSTable.HrFSEntries, Has.Count.EqualTo(1));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSIndex, Is.EqualTo(new Integer32(6)));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSMountPoint, Is.EqualTo(new OctetString("MountingPoint")));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSRemoteMountPoint, Is.EqualTo(new OctetString("RemoteMountingPoint")));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSType, Is.EqualTo(new ObjectIdentifier("1.4")));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSAccess, Is.EqualTo(new Integer32(7)));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSBootable, Is.EqualTo(new Integer32(8)));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSStorageIndex, Is.EqualTo(new Integer32(9)));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSLastFullBackupDate, Is.EqualTo(new OctetString("LastFullBackupDate")));
        Assert.That(result.HrFSTable.HrFSEntries[0].HrFSLastPartialBackupDate, Is.EqualTo(new OctetString("LastPartialBackupDate")));
    }
}