using GreenhouseManagement.Server.DTO;
using GreenhouseManagement.Server.Models;

namespace GreenhouseManagement.Server.Repository;

public class SensorManagerRepository : ISensorManagerRepository
{
    private readonly List<SensorManager> _sensorValues = [];

    public SensorManager? GetSensorById(Guid id)
    {
        return this._sensorValues.Find(sensor => sensor.Id == id);
    }

    public List<SensorManager> GetSensors()
    {
        return this._sensorValues;
    }

    public void StoreSensor(SensorManager sensor)
    {
        this._sensorValues.Add(sensor);
    }
}
