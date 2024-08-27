using GreenhouseManagement.Server.DTO;
using GreenhouseManagement.Server.Models;
using GreenhouseManagement.Server.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GreenhouseManagement.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorsController : ControllerBase
{
    private readonly ISensorValueRepository _sensorValueRepository;
    private readonly ISensorManagerRepository _sensorManagerRepository;

    public SensorsController(ISensorValueRepository sensorValueRepository, ISensorManagerRepository sensorManagerRepository)
    {
        this._sensorValueRepository = sensorValueRepository;
        this._sensorManagerRepository = sensorManagerRepository;
    }

    [HttpGet("data")]
    public IActionResult GetSensorsData()
    {
        return Ok(this._sensorValueRepository.GetSensorValues());
    }

    [HttpGet("data/{sensorId}")]
    public IActionResult GetSensorsData(string sensorId)
    {
        return Ok(this._sensorValueRepository.GetSensorValuesBySensorId(Guid.Parse(sensorId)));
    }

    [HttpPost("data")]
    public IActionResult PostSensorsData(List<SensorValueDTO> sensorValues, string sensorId)
    {
        if (this._sensorManagerRepository.GetSensorById(Guid.Parse(sensorId)) == null)
        {
            return NotFound($"Sensor {sensorId} not registered");
        }

        foreach (var sensorValue in sensorValues)
        {
            this._sensorValueRepository.StoreValue(
                new SensorValue(
                    Guid.Parse(sensorId),
                    sensorValue.Value,
                    sensorValue.MeasureUnit,
                    sensorValue.SensorType
                )
            );
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult RegisterSensorManager(SensorManagerDTO sensor)
    {
        var sensorManager = new SensorManager(sensor.Id, sensor.Url);
        this._sensorManagerRepository.StoreSensor(sensorManager);
        return Ok();
    }
}
