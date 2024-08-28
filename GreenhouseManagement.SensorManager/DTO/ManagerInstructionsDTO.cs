using GreenhouseManagement.SensorManager.Models.Actuators;
using GreenhouseManagement.SensorManager.Models.Sensors;

namespace GreenhouseManagement.SensorManager.DTO;

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