using System.Security.Cryptography;

namespace GreenhouseManagement.SensorManager.Models;

public class HumiditySensor : ISensor
{
    public string GetMeasureUnit()
    {
        return "hum";
    }

    public SensorType GetSensorType()
    {
        return SensorType.Humidity;
    }

    public decimal ReadValue()
    {
        var rand = new Random();
        return new decimal(rand.NextDouble());
    }
}

