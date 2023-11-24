using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.Data.Services.Read.Component.Disk;
using Netmon.DeviceManager.DTO.Device.Statistics;
using Netmon.Models.Component.Disk;

namespace Netmon.DeviceManager.Controllers.Device.Statistics;

[ApiController]
[Route("device/{id}/statistics/disk")]
public class DeviceDiskStatisticsController : BaseController
{

    private readonly IDiskReadService _diskReadService;
    
    public DeviceDiskStatisticsController(IDiskReadService diskReadService)
    {
        _diskReadService = diskReadService;
    } 
    
    [HttpGet("")]
    public async Task<IActionResult> GetDiskStatisticsAsync(Guid id, DateTime fromDate, DateTime toDate)
    {
        List<IDisk> disks = (await _diskReadService.GetByDeviceIdWithMetrics(id, fromDate, toDate)).ToList();
        DeviceDisksStatisticsDTO dto = DeviceDisksStatisticsDTO.FromDisks(disks);
        return Ok(dto);
    }
    
}