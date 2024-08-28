namespace GreenhouseManagement.SensorManager.Models.Sensors;

public class LightSensor : ISensor
{
    public string GetMeasureUnit()
    {
        return "ln";
    }

    public SensorType GetSensorType()
    {
        return SensorType.Light;
    }

    public decimal ReadValue()
    {
        var rand = new Random();
        return new decimal(rand.NextDouble() * 100);
    }
}