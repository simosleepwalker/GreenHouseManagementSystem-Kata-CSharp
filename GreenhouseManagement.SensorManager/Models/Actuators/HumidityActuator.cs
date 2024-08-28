namespace GreenhouseManagement.SensorManager.Models.Actuators;

public class HumidityActuator : IActuator
{
    public void DecreaseValue()
    {
        Console.WriteLine("Opening glass to decrease humidity");
    }

    public void IncreaseValue()
    {
        Console.WriteLine("Closing glass to increase humidity");
    }
}
