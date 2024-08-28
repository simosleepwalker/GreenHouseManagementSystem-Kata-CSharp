namespace GreenhouseManagement.SensorManager.Models.Actuators;

public class HumidityActuator : IActuator
{
    public void DecreaseValue()
    {
        Console.WriteLine("Turning on dehumidifier to decrease humidity");
    }

    public void IncreaseValue()
    {
        Console.WriteLine("Watering plants to increase humidity");
    }
}
