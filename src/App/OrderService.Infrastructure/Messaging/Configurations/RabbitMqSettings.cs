namespace OrderService.Infrastructure.Messaging.Configurations;

public class RabbitMqSettings
{
    public const string SectionName = "RabbitMQ";
    public string Host { get; set; } = "";
    public int Port { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}