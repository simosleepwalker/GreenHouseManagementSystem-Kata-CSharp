using GreenhouseManagement.SensorManager.Models.Actuators;
using GreenhouseManagement.SensorManager.Models.Sensors;
using ConfigurationManager = System.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var lightActuator = new LightActuator();
var temperatureActuator = new TemperatureActuator();
var humidityActuator = new HumidityActuator();
builder.Services.AddSingleton(lightActuator);
builder.Services.AddSingleton(temperatureActuator);
builder.Services.AddSingleton(humidityActuator);
builder.Services.AddSingleton(new ActuatorFactory(temperatureActuator, humidityActuator, lightActuator));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var sensorManager = new SensorManager();
sensorManager.AddSensor(new HumiditySensor());
sensorManager.AddSensor(new LightSensor());
sensorManager.AddSensor(new TemperatureSensor());

await sensorManager.RegisterSensor();
sensorManager.Start();

app.Run(ConfigurationManager.AppSettings["Greenhouse:SensorManagerBaseURL"]);