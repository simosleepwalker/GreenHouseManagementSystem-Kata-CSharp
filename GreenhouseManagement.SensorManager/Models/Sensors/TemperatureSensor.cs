namespace GreenhouseManagement.SensorManager.Models.Sensors;

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
        return new decimal(rand.NextDouble() * 100);
    }
}