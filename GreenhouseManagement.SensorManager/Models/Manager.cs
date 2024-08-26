using System.Text;
using System.Text.Json.Nodes;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;

namespace GreenhouseManagement.SensorManager.Models;

public class Manager
{
    public List<ISensor> Sensors { get; set; }
    private readonly Guid _id;

    public Manager()
    {
        this._id = Guid.NewGuid();
        this.Sensors = [];
    }

    public void AddSensor(ISensor sensor)
    {
        this.Sensors.Add(sensor);
    }

    public void RemoveSensor(ISensor sensor)
    {
        this.Sensors.Remove(sensor);
    }

    public List<ISensor> GetSensors()
    {
        return this.Sensors;
    }

    private List<SensorValue> GetSensorValues()
    {
        List<SensorValue> sensorValues = [];

        foreach (ISensor sensor in this.Sensors)
        {
            sensorValues.Add(new SensorValue(sensor.ReadValue(), sensor.GetMeasureUnit(), sensor.GetSensorType()));
        }

        return sensorValues;
    }

    public async Task SendValues()
    {
        var client = new HttpClient();

        List<SensorValue> values = this.GetSensorValues();

        if (values.Count == 0)
        {
            return;
        }

        string json = JsonConvert.SerializeObject(values);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await client.PostAsync($"http://localhost:5129/api/sensors?sensorId={this._id}", content);

        Console.WriteLine($"Values {json} sent to server");
    }

    public async Task Start()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(1000);
            await this.SendValues();
        }
    }
}