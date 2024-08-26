namespace GreenhouseManagement.SensorManager.Models;

public class TemperatureSensor : ISensor
{
    public string GetMeasureUnit()
    {
        return "C";
    }

    public SensorType GetSensorType()
    {
        return SensorType.Temperature;
    }

    public decimal ReadValue()
    {
        var rand = new Random();
        return new decimal(rand.NextDouble());
    }
}