using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.Data.Services.Read.Component.Interface;
using Netmon.DeviceManager.DTO.Device.Statistics;
using Netmon.Models.Component.Interface;

namespace Netmon.DeviceManager.Controllers.Device.Statistics;

[ApiController]
[Route("device/{id}/statistics/interface")]
public class DeviceInterfaceStatisticsController : BaseController
{
    private readonly IInterfaceReadService _interfaceReadService;
    
    public DeviceInterfaceStatisticsController(IInterfaceReadService interfaceReadService)
    {
        _interfaceReadService = interfaceReadService;
    }   
    
    [HttpGet("inout")]
    public async Task<IActionResult> GetInterfaceStatisticsAsync(Guid id, DateTime fromDate, DateTime toDate)
    {
        List<IInterface> interfaces = (await _interfaceReadService.GetByDeviceIdWithMetrics(id, fromDate, toDate)).ToList();
        DeviceInterfacesStatisticsDTO dto = DeviceInterfacesStatisticsDTO.FromInterfaces(interfaces);
        return Ok(dto);
    }
}