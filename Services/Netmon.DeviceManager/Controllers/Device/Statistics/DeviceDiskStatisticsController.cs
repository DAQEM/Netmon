using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.DeviceManager.DTO.Device.Statistics;
using Netmon.Models.Component.Disk;

namespace Netmon.DeviceManager.Controllers.Device.Statistics;

[ApiController]
[Route("device/{id}/statistics/disk")]
public class DeviceDiskStatisticsController : BaseController
{
    
    private readonly IDiskReadRepository _diskReadRepository;
    
    public DeviceDiskStatisticsController(IDiskReadRepository diskReadRepository)
    {
        _diskReadRepository = diskReadRepository;
    } 
    
    [HttpGet("")]
    public async Task<IActionResult> GetDiskStatisticsAsync(Guid id, DateTime fromDate, DateTime toDate)
    {
        List<IDisk> disks = (await _diskReadRepository.GetByDeviceIdWithMetrics(id, fromDate, toDate)).Select(dbo => dbo.ToDisk()).ToList();
        DeviceDisksStatisticsDTO dto = DeviceDisksStatisticsDTO.FromDisks(disks);
        return Ok(dto);
    }
    
}