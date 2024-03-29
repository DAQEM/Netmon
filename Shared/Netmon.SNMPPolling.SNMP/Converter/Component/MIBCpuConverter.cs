﻿using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;
using Netmon.Models.Component.Cpu.Metric;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis.CpuLoad;

namespace Netmon.SNMPPolling.SNMP.Converter.Component;

public class MIBCpuConverter : IMIBComponentConverter<ICpu>
{
    public List<ICpu> ConvertMIBsToComponent(List<IMIB> mibs)
    {
        Cpu? cpu = new()
        {
            Index = 1,
            Metrics = new List<ICpuMetric>(),
            Cores = new List<ICpuCore>()
        };

        HostResourcesMIB? hostResourcesMIB = mibs.OfType<HostResourcesMIB>().FirstOrDefault();
        
        if (hostResourcesMIB is not null && hostResourcesMIB.HrDevice.HrProcessorTable.HrProcessorEntries.Any())
        {
            cpu.Cores = hostResourcesMIB.HrDevice.HrProcessorTable.HrProcessorEntries
                .Select(e => new CpuCore
                {
                    Index = e.HrProcessorIndex.ToInt32(),
                    Name = hostResourcesMIB.HrDevice.HrDeviceTable.HrDeviceEntries
                        .FirstOrDefault(e1 => e1.HrDeviceIndex.ToInt32() == e.HrProcessorIndex.ToInt32())?.HrDeviceDescr
                        .ToString() ?? "Unknown",
                    Metrics = new List<ICpuCoreMetric>
                    {
                        new CpuCoreMetric
                        {
                            Load = e.HrProcessorLoad.ToInt32()
                        }
                    }
                })
                .Cast<ICpuCore>()
                .ToList();
        }
        
        UCDavisMIB? ucDavisMIB = mibs.OfType<UCDavisMIB>().FirstOrDefault();

        if (ucDavisMIB is not null && ucDavisMIB.LaLoadTable.LaLoadEntries.Any())
        {
            List<LaLoadEntry> laLoadEntries = ucDavisMIB.LaLoadTable.LaLoadEntries;
            CpuMetric cpuMetric = new()
            {
                OneMinuteLoad = laLoadEntries.Count > 0 ? laLoadEntries[0].LaLoadInt.ToInt32() : 0,
                FiveMinuteLoad = laLoadEntries.Count > 1 ? laLoadEntries[1].LaLoadInt.ToInt32() : 0,
                FifteenMinuteLoad = laLoadEntries.Count > 2 ? laLoadEntries[2].LaLoadInt.ToInt32() : 0
            };
            cpu.Metrics = new List<ICpuMetric> { cpuMetric };
        }
        
        return !cpu.Cores.Any() && !cpu.Metrics.Any() ? new List<ICpu>() : new List<ICpu> { cpu };
    }
}