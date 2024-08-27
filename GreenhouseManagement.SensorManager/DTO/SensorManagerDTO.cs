namespace GreenhouseManagement.SensorManager.DTO;

public class SensorManagerDTO
{
    public Guid Id { get; set; }
    public string Url { get; set; }

    public SensorManagerDTO(Guid id, string url)
    {
        this.Id = id;
        this.Url = url;
    }
}