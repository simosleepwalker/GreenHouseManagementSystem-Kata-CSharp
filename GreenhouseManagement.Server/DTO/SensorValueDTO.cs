using GreenhouseManagement.Server.Models;

namespace GreenhouseManagement.Server.DTO;

public class SensorValueDTO
{
    public decimal Value { get; set; }
    public string MeasureUnit { get; set; }
    public SensorType SensorType { get; set; }

    public SensorValueDTO(decimal value, string measureUnit, SensorType sensorType)
    {
        this.Value = value;
        this.MeasureUnit = measureUnit;
        this.SensorType = sensorType;
    }
}