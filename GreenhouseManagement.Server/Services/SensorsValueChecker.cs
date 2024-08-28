using System.Runtime.CompilerServices;
using System.Text;
using GreenhouseManagement.Server.Models;
using GreenhouseManagement.Server.Repository;
using Newtonsoft.Json;

namespace GreenhouseManagement.Server.Services;

public class SensorValueChecker
{
    private readonly ISensorValueRepository _sensorValueRepository;
    private readonly SensorManager _sensorManager;

    public SensorValueChecker(ISensorValueRepository sensorValueRepository, SensorManager sensorManager)
    {
        this._sensorManager = sensorManager;
        this._sensorValueRepository = sensorValueRepository;
    }

    private ManagerInstructionsDTO BuildAction(SensorValue sensorValue)
    {
        if (sensorValue.SensorType == SensorType.Temperature)
        {
            return new ManagerInstructionsDTO(SensorType.Temperature, sensorValue.Value > 20 ? ActuatorAction.Decrease : ActuatorAction.Increase);
        }
        if (sensorValue.SensorType == SensorType.Humidity)
        {
            return new ManagerInstructionsDTO(SensorType.Humidity, sensorValue.Value > 50 ? ActuatorAction.Decrease : ActuatorAction.Increase);
        }

        return new ManagerInstructionsDTO(SensorType.Light, sensorValue.Value > 50 ? ActuatorAction.Decrease : ActuatorAction.Increase);
    }

    private List<ManagerInstructionsDTO> CheckValues()
    {
        var sensorValues = new List<SensorValue?>
        {
            this._sensorValueRepository.GetLatestSensorValueByType(this._sensorManager.Id, SensorType.Temperature),
            this._sensorValueRepository.GetLatestSensorValueByType(this._sensorManager.Id, SensorType.Humidity),
            this._sensorValueRepository.GetLatestSensorValueByType(this._sensorManager.Id, SensorType.Light)
        }.ToList();

        List<ManagerInstructionsDTO> instructions = [];

        foreach (var value in sensorValues)
        {
            if (value == null)
            {
                continue;
            }
            instructions.Add(this.BuildAction(value));
        }

        return instructions;
    }

    private async Task SendInstructionsToManager()
    {
        List<ManagerInstructionsDTO> managerInstructions = this.CheckValues();

        var client = new HttpClient();

        foreach (var instruction in managerInstructions)
        {
            string json = JsonConvert.SerializeObject(instruction);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{this._sensorManager.Url}/actuators", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to send instructions to manager");
                return;
            }
        }
    }

    public async Task Start()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(10000);
            await this.SendInstructionsToManager();
        }
    }
}