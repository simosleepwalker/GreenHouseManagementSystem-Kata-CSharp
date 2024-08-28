namespace GreenhouseManagement.SensorManager.Models.Sensors;

public class SensorValue
{
    public decimal Value { get; set; }
    public string MeasureUnit { get; set; }
    public SensorType SensorType { get; set; }

    public SensorValue(decimal value, string measureUnit, SensorType sensorType)
    {
        this.Value = value;
        this.MeasureUnit = measureUnit;
        this.SensorType = sensorType;
    }
}