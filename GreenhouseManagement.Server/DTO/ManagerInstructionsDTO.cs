using GreenhouseManagement.Server.Models;

namespace GreenhouseManagement.Server.Services;

public class ManagerInstructionsDTO
{
    public SensorType SensorType { get; set; }
    public ActuatorAction ActuatorAction { get; set; }

    public ManagerInstructionsDTO(SensorType sensorType, ActuatorAction actuatorAction)
    {
        this.SensorType = sensorType;
        this.ActuatorAction = actuatorAction;
    }
}