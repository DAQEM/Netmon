using System.Net;
using System.Net.Sockets;
using DeviceManagerService.Models.Device;
using DevicesLib.Database;
using DevicesLib.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeviceManagerService.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
    private readonly DevicesDatabase _database;
    
    public DeviceController(DevicesDatabase database)
    {
        _database = database;
    }

    [HttpGet]
    public IActionResult All()
    {
        return new JsonResult(
            _database.Devices.Select(d => new GetDeviceModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                IpAddress = d.IpAddress,
                Community = d.Community,
                Location = d.Location,
                Contact = d.Contact
            })
        );
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        DeviceDTO? device = _database.Devices.Find(id);

        if (device == null)
        {
            return new JsonResult(
                new
                {
                    error = "Device not found"
                }
            );
        }
        
        return new JsonResult(
            new GetDeviceModel
            {
                Id = device.Id,
                Name = device.Name,
                Description = device.Description,
                IpAddress = device.IpAddress,
                Community = device.Community,
                Location = device.Location,
                Contact = device.Contact
            }
        );
    }

    [HttpPost]
    public IActionResult Create(CreateDeviceModel model)
    {
        Dictionary<string, List<string>> errors = new();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (errors.Any())
        {
            Response.StatusCode = 400;
            return new JsonResult(
                new
                {
                    errors
                }
            );   
        }
        
        Guid deviceId = Guid.NewGuid();
        
        DeviceDTO device = new()
        {
            Id = deviceId,
            Name = model.Name,
            Description = model.Description,
            IpAddress = model.IpAddress,
            Community = model.Community,
            Location = model.Location,
            Contact = model.Contact
        };
        
        _database.Devices.Add(device);
        List<DeviceOIDDTO> defaultDeviceOiDs = DeviceOidController.GetDefaultDeviceOiDs(deviceId);
        _database.DeviceOIDs.AddRange(defaultDeviceOiDs);
        _database.SaveChanges();

        return new JsonResult(
            new
            {
                model
            }
        );
    }
}