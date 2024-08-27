using System.Text;
using GreenhouseManagement.SensorManager.DTO;
using Microsoft.AspNetCore.Server.HttpSys;
using Newtonsoft.Json;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace GreenhouseManagement.SensorManager.Models;

public class Manager
{
    public List<ISensor> Sensors { get; set; }
    private readonly Guid _id;
    private readonly string _serverBaseUrl;
    private readonly string _sensorManagerBaseUrl;
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
        this._serverBaseUrl = baseUrl;

        var parseResult = int.TryParse(ConfigurationManager.AppSettings["Greenhouse:SensorInterval"], out int interval);
        if (!parseResult)
        {
            interval = 10000;
        }
        this._interval = interval;

        var sensorManagerBaseUrl = ConfigurationManager.AppSettings["Greenhouse:SensorManagerBaseURL"];
        if (sensorManagerBaseUrl == null || sensorManagerBaseUrl == "")
        {
            throw new Exception("No BaseUrl configured for the Greenhouse sensor manager");
        }
        this._sensorManagerBaseUrl = sensorManagerBaseUrl;
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

    public async Task RegisterSensor()
    {
        var client = new HttpClient();

        var sensorManagerDTO = new SensorManagerDTO(this._id, this._sensorManagerBaseUrl);
        string json = JsonConvert.SerializeObject(sensorManagerDTO);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{this._serverBaseUrl}/api/sensors", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Couldn't register sensor manager to GreenHouse Server");
        }

        Console.WriteLine("Sensor manager registered to GreenHouse Server");

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

        var response = await client.PostAsync($"{this._serverBaseUrl}/api/sensors/data?sensorId={this._id}", content);

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