namespace GreenhouseManagement.SensorManager.Models.Sensors;

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