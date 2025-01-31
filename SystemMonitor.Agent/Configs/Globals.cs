namespace SystemMonitor.Agent.Configs;

public class Globals
{
    public static string GetComputerName()
    {
        return Environment.MachineName;
    }

    public static string GetUserName()
    {
        return Environment.UserName;
    }
}