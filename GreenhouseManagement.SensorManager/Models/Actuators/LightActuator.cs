namespace GreenhouseManagement.SensorManager.Models.Actuators;

public class LightActuator : IActuator
{
    public void DecreaseValue()
    {
        Console.WriteLine("Decreasing light level");
    }

    public void IncreaseValue()
    {
        Console.WriteLine("Increasing light level");
    }
}
