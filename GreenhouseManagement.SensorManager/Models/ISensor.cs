namespace GreenhouseManagement.SensorManager.Models;

public interface ISensor
{
    public decimal ReadValue();
    public string GetMeasureUnit();
    public SensorType GetSensorType();
}

public enum SensorType
{
    Temperature,
    Humidity,
    Light,
}