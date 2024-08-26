using GreenhouseManagement.SensorManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

var sensorManager = new Manager();
sensorManager.AddSensor(new HumiditySensor());
sensorManager.AddSensor(new LightSensor());
sensorManager.AddSensor(new TemperatureSensor());

sensorManager.Start();

app.Run();