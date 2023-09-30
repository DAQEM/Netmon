using SNMPPollingService.Entities.Component.Cpu;
using SNMPPollingService.Entities.Component.Cpu.Core;
using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.MIB.HostResources;
using SNMPPollingService.SNMP.MIB.UCDavis;
using SNMPPollingService.SNMP.MIB.UCDavis.CpuLoad;

namespace SNMPPollingService.SNMP.Converter.Component;

public class MIBCpuConverter : IMIBComponentConverter<ICpu>
{
    public List<ICpu> ConvertMIBsToComponent(List<IMIB> mibs)
    {
        Cpu cpu = new();
        
        HostResourcesMIB? hostResourcesMIB = mibs.OfType<HostResourcesMIB>().FirstOrDefault();
        
        if (hostResourcesMIB != null)
        {
            cpu.Cores = hostResourcesMIB.HrDevice.HrProcessorTable.HrProcessorEntries
                .Select(e => new CpuCore(
                    hostResourcesMIB.HrDevice.HrDeviceTable.HrDeviceEntries
                        .FirstOrDefault(e1 => e1.HrDeviceIndex.ToInt32() == e.HrProcessorOID.ToInt32())?.HrDeviceDescr
                        .ToString() ?? "Unknown",
                    e.HrProcessorLoad.ToInt32()
                ))
                .Cast<ICpuCore>()
                .ToList();
        }
        
        UCDavisMIB? ucDavisMIB = mibs.OfType<UCDavisMIB>().FirstOrDefault();

        if (ucDavisMIB != null)
        {
            List<LaLoadEntry> laLoadEntries = ucDavisMIB.LaLoadTable.LaLoadEntries;
            cpu.OneMinuteLoad = laLoadEntries.Count > 0 ? laLoadEntries[0].LaLoadInt.ToInt32() : 0;
            cpu.FiveMinuteLoad = laLoadEntries.Count > 1 ? laLoadEntries[1].LaLoadInt.ToInt32() : 0;
            cpu.FifteenMinuteLoad = laLoadEntries.Count > 2 ? laLoadEntries[2].LaLoadInt.ToInt32() : 0;
        }
        
        return new List<ICpu> { cpu };
    }
}