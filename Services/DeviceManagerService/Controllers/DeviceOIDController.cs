using DeviceManagerService.Models.DeviceOID;
using DevicesLib.Database;
using DevicesLib.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagerService.Controllers;

[ApiController]
[Route("/Device/{deviceId}/[controller]")]
public class DeviceOidController : ControllerBase
{
    private readonly DevicesDatabase _database;
    
    public DeviceOidController(DevicesDatabase database)
    {
        _database = database;
    }
    
    [HttpGet]
    public IActionResult All(Guid deviceId)
    {
        DeviceDTO? device = _database.Devices
            .Include(d => d.OIDs)
            .FirstOrDefault(d => d.Id.Equals(deviceId));

        if (device == null) 
        {
            Response.StatusCode = 404;
            return new JsonResult(
                new
                {
                    error = "Device not found"
                }
            );
        }
        
        return new JsonResult(
            device.OIDs.Select(
                oid => new GetDeviceOIDModel
                {
                    Id = oid.Id,
                    DeviceId = oid.DeviceId,
                    Type = oid.Type,
                    Value = oid.Value
                }
            )
        );
    }

    public static List<DeviceOIDDTO> GetDefaultDeviceOiDs(Guid deviceId)
    {
        Dictionary<DeviceOIDDTO.DeviceOIDType, string> defaultOiDs = new()
        {
            {DeviceOIDDTO.DeviceOIDType.StorageDescription, "1.3.6.1.2.1.25.2.3.1.3"},
            {DeviceOIDDTO.DeviceOIDType.StorageAllocationUnit, "1.3.6.1.2.1.25.2.3.1.4"},
            {DeviceOIDDTO.DeviceOIDType.StorageTotal, "1.3.6.1.2.1.25.2.3.1.5"},
            {DeviceOIDDTO.DeviceOIDType.StorageUsed, "1.3.6.1.2.1.25.2.3.1.6"},
        };

        return defaultOiDs.Select(
            pair => new DeviceOIDDTO
            {
                DeviceId = deviceId,
                Type = pair.Key,
                Value = pair.Value
            }
        ).ToList();
    }
}