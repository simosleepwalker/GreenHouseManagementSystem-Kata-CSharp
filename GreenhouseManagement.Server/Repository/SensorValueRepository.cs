namespace GreenhouseManagement.Server.Repository;

using GreenhouseManagement.Server.Models;

public class SensorValueRepository : ISensorValueRepository
{
    private readonly List<SensorValue> _sensorValues = [];

    public List<SensorValue> GetSensorValues()
    {
        return this._sensorValues;
    }

    public List<SensorValue> GetSensorValuesBySensorId(Guid sensorId)
    {
        return this._sensorValues.Where(x => x.SensorId == sensorId).ToList();
    }

    public void StoreValue(SensorValue sensorValue)
    {
        this._sensorValues.Add(sensorValue);
    }

    public SensorValue? GetLatestSensorValue(Guid sensorId)
    {
        return this._sensorValues
            .Where(x => x.SensorId == sensorId)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefault();
    }

    public SensorValue? GetLatestSensorValueByType(Guid sensorId, SensorType sensorType)
    {
        return this._sensorValues
            .Where(x => x.SensorId == sensorId && x.SensorType == sensorType)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefault();
    }
}
