using GreenhouseManagement.Server.Models;

namespace GreenhouseManagement.Server.Repository;

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
}
