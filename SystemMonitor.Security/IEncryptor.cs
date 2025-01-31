namespace SystemMonitor.Security;

public interface IEncryptor
{
    string PasswordEncrypt(string value);
}