namespace GreenhouseManagement.Server.Models;

public class SensorManager
{
    public Guid Id { get; set; }
    public string Url { get; set; }

    public SensorManager(Guid id, string url)
    {
        this.Id = id;
        this.Url = url;
    }
}