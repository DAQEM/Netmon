using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Services.Read.Component.Interface;
using Netmon.DeviceManager.DTO.Device.Statistics;
using Netmon.Models.Component.Interface;

namespace Netmon.DeviceManager.Controllers.Device.Statistics;

[ApiController]
[Route("device/{id}/statistics/interface")]
public class DeviceInterfaceStatisticsController(IInterfaceReadService interfaceReadService) : BaseController
{
    [HttpGet("inout")]
    public async Task<IActionResult> GetInterfaceStatisticsAsync(Guid id, DateTime fromDate, DateTime toDate)
    {
        List<IInterface> interfaces = (await interfaceReadService.GetByDeviceIdWithMetrics(id, fromDate, toDate)).ToList();
        DeviceInterfacesStatisticsDTO dto = DeviceInterfacesStatisticsDTO.FromInterfaces(interfaces);
        return Ok(dto);
    }
}