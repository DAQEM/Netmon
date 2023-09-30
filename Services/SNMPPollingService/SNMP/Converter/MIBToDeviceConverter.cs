using SNMPPollingService.Entities.Component;
using SNMPPollingService.Entities.Component.Cpu;
using SNMPPollingService.Entities.Component.Cpu.Core;
using SNMPPollingService.Entities.Component.Disk;
using SNMPPollingService.Entities.Component.Interface;
using SNMPPollingService.Entities.Component.Memory;
using SNMPPollingService.Entities.Device;
using SNMPPollingService.SNMP.MIB.HostResources;
using SNMPPollingService.SNMP.MIB.HostResources.Storage;
using SNMPPollingService.SNMP.MIB.If;
using SNMPPollingService.SNMP.MIB.If.InterfaceX;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.MIB.UCDavis;
using SNMPPollingService.SNMP.MIB.UCDavis.CpuLoad;
using SNMPPollingService.SNMP.Request;
using EntityType = SNMPPollingService.Entities.EntityType;

namespace SNMPPollingService.SNMP.Converter;

public static class MIBToDeviceConverter
{
    public static IDevice ConvertToDevice(SNMPConnectionInfo connectionInfo, SystemMIB systemMIB, HostResourcesMIB hostResourcesMIB, UCDavisMIB ucDavisMIB, IfMIB ifMIB)
    {
        IDevice device = new Device(
            connectionInfo.IpAddress,
            connectionInfo.Port,
            connectionInfo.Community,
            systemMIB.SysName.ToString(),
            systemMIB.SysLocation.ToString(),
            systemMIB.SysContact.ToString()
        );

        List<IDisk> disks = hostResourcesMIB.HrStorage.HrStorageTable.HrStorageEntries
            .Where(e => e.HrStorageType == HrStorageEntry.StorageType.FixedDisk)
            .Select(e => new Disk(
                e.HrStorageDescr.ToString(),
                e.HrStorageAllocationUnits.ToInt32(),
                e.HrStorageSize.ToInt32(),
                e.HrStorageUsed.ToInt32()
            ))
            .Cast<IDisk>()
            .ToList();
        
        device.Components.Add(EntityType.Disk, disks.Cast<IComponent>().ToList());
        
        List<ICpuCore> cpuCores = hostResourcesMIB.HrDevice.HrProcessorTable.HrProcessorEntries
            .Select(e => new CpuCore(
                hostResourcesMIB.HrDevice.HrDeviceTable.HrDeviceEntries.FirstOrDefault(e1 => e1.HrDeviceIndex.ToInt32() == e.HrProcessorOID.ToInt32())?.HrDeviceDescr.ToString() ?? "Unknown",
                e.HrProcessorLoad.ToInt32()
            ))
            .Cast<ICpuCore>()
            .ToList();

        List<LaLoadEntry> laLoadEntries = ucDavisMIB.LaLoadTable.LaLoadEntries;
        Cpu cpu = new(
            laLoadEntries.Count > 0 ? laLoadEntries[0].LaLoadInt.ToInt32() : 0,
            laLoadEntries.Count > 1 ? laLoadEntries[1].LaLoadInt.ToInt32() : 0,
            laLoadEntries.Count > 2 ? laLoadEntries[2].LaLoadInt.ToInt32() : 0,
            cpuCores
        );
        
        device.Components.Add(EntityType.Cpu, new List<IComponent> { cpu });
        
        List<IMemory> memories = hostResourcesMIB.HrStorage.HrStorageTable.HrStorageEntries
            .Where(e => e.HrStorageType == HrStorageEntry.StorageType.Ram)
            .Select(e => new Memory(
                e.HrStorageDescr.ToString(),
                e.HrStorageAllocationUnits.ToInt32(),
                e.HrStorageSize.ToInt32(),
                e.HrStorageUsed.ToInt32()
            ))
            .Cast<IMemory>()
            .ToList();
        
        device.Components.Add(EntityType.Memory, memories.Cast<IComponent>().ToList());
        
        List<IInterface> interfaces = ifMIB.IfTable.IfEntries
            .Select(e =>
            {
                IfXEntry ifXEntry = ifMIB.IfXTable.IfXEntries.FirstOrDefault(e1 => e1.IfIndex == e.IfIndex.ToInt32()) ?? new IfXEntry();
                return new Interface(
                    e.IfIndex.ToInt32(),
                    e.IfDescr.ToString(),
                    e.IfType,
                    e.IfAdminStatus,
                    e.IfOperationalStatus,
                    e.IfSpeed.ToUInt32(),
                    e.IfPhysAddress.ToString(),
                    e.IfMtu.ToInt32(),
                    ifXEntry.IfHCInOctets.ToUInt64(),
                    ifXEntry.IfHCOutOctets.ToUInt64(),
                    e.IfInErrors.ToUInt32(),
                    e.IfOutErrors.ToUInt32(),
                    e.IfInDiscards.ToUInt32(),
                    e.IfOutDiscards.ToUInt32(),
                    ifXEntry.IfHCInBroadcastPkts.ToUInt64(),
                    ifXEntry.IfHCOutBroadcastPkts.ToUInt64(),
                    ifXEntry.IfHCInMulticastPkts.ToUInt64(),
                    ifXEntry.IfHCOutMulticastPkts.ToUInt64(),
                    ifXEntry.IfHCInUcastPkts.ToUInt64(),
                    ifXEntry.IfHCOutUcastPkts.ToUInt64()
                        
                );
            })
            .Cast<IInterface>()
            .ToList();
        
        device.Components.Add(EntityType.Interface, interfaces.Cast<IComponent>().ToList());
        
        return device;
    }
}