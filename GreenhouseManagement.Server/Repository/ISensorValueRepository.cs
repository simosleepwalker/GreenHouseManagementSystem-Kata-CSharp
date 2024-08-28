namespace GreenhouseManagement.Server.Repository;

using GreenhouseManagement.Server.Models;

public interface ISensorValueRepository
{
    List<SensorValue> GetSensorValues();
    void StoreValue(SensorValue sensorValue);
    List<SensorValue> GetSensorValuesBySensorId(Guid sensorId);
    SensorValue? GetLatestSensorValue(Guid sensorId);
    SensorValue? GetLatestSensorValueByType(Guid sensorId, SensorType sensorType);
}