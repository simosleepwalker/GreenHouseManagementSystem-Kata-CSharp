namespace GreenhouseManagement.SensorManager.Models.Actuators;

public class TemperatureActuator : IActuator
{
    public void DecreaseValue()
    {
        Console.WriteLine("Turning heat off");
    }

    public void IncreaseValue()
    {
        Console.WriteLine("Turning heat on");
    }
}
