using Hangfire;
using Netmon.DeviceManager.Jobs.Poll;

namespace Netmon.DeviceManager.Jobs;

public class JobRegister
{
    private readonly IPollDeviceJob pollDeviceJob;

    public JobRegister(IPollDeviceJob pollDeviceJob)
    {
        this.pollDeviceJob = pollDeviceJob;
    }

    public void RegisterJobs()
    {
        RecurringJob.AddOrUpdate("poll", () => pollDeviceJob.Execute(), "*/5 * * * *");
    }
}