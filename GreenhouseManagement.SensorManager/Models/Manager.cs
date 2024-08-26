using System.Text;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace GreenhouseManagement.SensorManager.Models;

public class Manager
{
    public List<ISensor> Sensors { get; set; }
    private readonly Guid _id;
    private readonly string _baseUrl;
    private readonly int _interval;

    public Manager()
    {
        this._id = Guid.NewGuid();
        this.Sensors = [];

        var baseUrl = ConfigurationManager.AppSettings["Greenhouse:ServerBaseURL"];
        if (baseUrl == null || baseUrl == "")
        {
            throw new Exception("No BaseUrl configured for the Greenhouse server");
        }
        this._baseUrl = baseUrl;

        var parseResult = int.TryParse(ConfigurationManager.AppSettings["Greenhouse:SensorInterval"], out int interval);
        if (!parseResult)
        {
            interval = 10000;
        }
        this._interval = interval;
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

        var response = await client.PostAsync($"{this._baseUrl}/api/sensors?sensorId={this._id}", content);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Failed to send values to server");
            return;
        }

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