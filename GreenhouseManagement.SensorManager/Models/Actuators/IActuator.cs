namespace GreenhouseManagement.SensorManager.Models.Actuators;

public interface IActuator
{
    public void IncreaseValue();
    public void DecreaseValue();
}

public enum ActuatorAction
{
    Increase,
    Decrease
}