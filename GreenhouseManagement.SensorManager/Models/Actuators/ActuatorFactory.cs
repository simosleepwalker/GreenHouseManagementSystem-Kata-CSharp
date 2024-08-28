using GreenhouseManagement.SensorManager.Models.Sensors;

namespace GreenhouseManagement.SensorManager.Models.Actuators;

public class ActuatorFactory
{
    private readonly TemperatureActuator _temperatureActuator;
    private readonly HumidityActuator _humidityActuator;
    private readonly LightActuator _lightActuator;

    public ActuatorFactory(TemperatureActuator temperatureActuator, HumidityActuator humidityActuator, LightActuator lightActuator)
    {
        this._temperatureActuator = temperatureActuator;
        this._humidityActuator = humidityActuator;
        this._lightActuator = lightActuator;
    }

    public IActuator GetActuator(SensorType sensorType)
    {
        return sensorType switch
        {
            SensorType.Temperature => this._temperatureActuator,
            SensorType.Humidity => this._humidityActuator,
            SensorType.Light => this._lightActuator,
            _ => throw new Exception("Sensor type does not exist"),
        };
    }
}