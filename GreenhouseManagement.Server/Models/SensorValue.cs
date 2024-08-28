namespace GreenhouseManagement.Server.Models;

public class SensorValue
{
    public Guid SensorId { get; }
    public decimal Value { get; set; }
    public string MeasureUnit { get; set; }
    public SensorType SensorType { get; set; }
    public DateTime Timestamp { get; set; }

    public SensorValue(Guid sensorId, decimal value, string measureUnit, SensorType sensorType)
    {
        this.SensorId = sensorId;
        this.Value = value;
        this.MeasureUnit = measureUnit;
        this.SensorType = sensorType;
        this.Timestamp = DateTime.Now;
    }
}

public enum SensorType
{
    Temperature,
    Humidity,
    Light,
}