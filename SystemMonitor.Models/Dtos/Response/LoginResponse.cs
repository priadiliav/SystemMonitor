namespace SystemMonitor.Models.Dtos.Response;

public class LoginResponse
{
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}