namespace SystemMonitor.Agent.Configs;
public class Globals
{
    public static string GetComputerName() => Environment.MachineName;
    public static string GetUserName() => Environment.UserName;
}
