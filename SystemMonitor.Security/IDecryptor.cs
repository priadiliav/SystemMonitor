namespace SystemMonitor.Security;

public interface IDecryptor
{
    string PasswordDecrypt(string value);
}
