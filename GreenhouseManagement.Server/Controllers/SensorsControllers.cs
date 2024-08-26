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

    public SensorsController(ISensorValueRepository sensorValueRepository)
    {
        this._sensorValueRepository = sensorValueRepository;
    }

    [HttpGet]
    public IActionResult GetSensorsData()
    {
        return Ok(this._sensorValueRepository.GetSensorValues());
    }

    [HttpGet("{sensorId}")]
    public IActionResult GetSensorsData(string sensorId)
    {
        return Ok(this._sensorValueRepository.GetSensorValuesBySensorId(Guid.Parse(sensorId)));
    }

    [HttpPost]
    public IActionResult PostSensorsData(List<SensorValueDTO> sensorValues, string sensorId)
    {
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
}
