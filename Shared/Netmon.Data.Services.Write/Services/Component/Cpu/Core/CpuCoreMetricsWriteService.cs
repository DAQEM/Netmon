﻿using Netmon.Data.Services.Write.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.Services.Write.Services.Component.Cpu.Core;

public class CpuCoreMetricsWriteService : ICpuCoreMetricsWriteService
{
    public Task Add(ICpuCoreMetric metric)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}