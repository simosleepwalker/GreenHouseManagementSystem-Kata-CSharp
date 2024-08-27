namespace GreenhouseManagement.Server.Repository;

using GreenhouseManagement.Server.DTO;
using GreenhouseManagement.Server.Models;

public interface ISensorManagerRepository
{
    public List<SensorManager> GetSensors();
    public SensorManager? GetSensorById(Guid id);
    public void StoreSensor(SensorManager sensor);
}