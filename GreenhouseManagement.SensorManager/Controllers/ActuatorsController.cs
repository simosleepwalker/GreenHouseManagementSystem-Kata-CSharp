using GreenhouseManagement.SensorManager.DTO;
using GreenhouseManagement.SensorManager.Models.Actuators;
using GreenhouseManagement.SensorManager.Models.Sensors;
using Microsoft.AspNetCore.Mvc;

namespace GreenhouseManagement.SensorManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ActuatorsController : ControllerBase
{
    private readonly ActuatorFactory _actuatorFactory;

    public ActuatorsController(ActuatorFactory _actuatorFactory)
    {
        this._actuatorFactory = _actuatorFactory;
    }

    [HttpPost]
    public IActionResult Actuate(ManagerInstructionsDTO managerInstructionsDTO)
    {
        IActuator actuator = this._actuatorFactory.GetActuator(managerInstructionsDTO.SensorType);
        if (managerInstructionsDTO.ActuatorAction == ActuatorAction.Increase)
        {
            actuator.IncreaseValue();
            return Ok();
        }

        actuator.DecreaseValue();
        return Ok();
    }
}