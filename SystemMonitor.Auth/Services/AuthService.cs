using AutoMapper;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.Models.Dtos.Request;
using SystemMonitor.Models.Dtos.Response;
using SystemMonitor.Models.Entities;
using SystemMonitor.Security;

namespace SystemMonitor.Auth.Services;

public class AuthService(
    ILogger<AuthService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IEncryptor encryptor,
    IDecryptor decryptor,
    JwtTokenService tokenService
    ) : BaseService<AuthService> (logger, mapper, unitOfWork)
{
    public async Task<RegisterResponse?> RegisterUser(RegisterRequest registerRequest)
    {
        if(registerRequest == null)
            throw new ArgumentNullException(nameof(registerRequest), "RegisterRequest can't be null");

        try
        {
            var existsUser = await userRepository.GetByUserName(registerRequest.Username);
            if(existsUser != null)
            {
                Logger.LogWarning("User with username {0} already exists", registerRequest.Username);
                return null;
            }
        
            var user = Mapper.Map<User>(registerRequest);
            user.Password = encryptor.PasswordEncrypt(user.Password);
        
            await userRepository.Add(user);
            await UnitOfWork.CompleteTask();    
            
            Logger.LogInformation("User with username {0} successfully registered", registerRequest.Username);
            
            return Mapper.Map<RegisterResponse>(user);
        }catch(Exception ex)
        {
            Logger.LogError(ex, "Error occurred");
            return null;
        }
    }
    public async Task<LoginResponse?> LoginUser(LoginRequest loginRequest)
    {
        if(loginRequest == null)
            throw new ArgumentNullException(nameof(loginRequest), "LoginRequest can't be null");

        try
        {
            var existsUser = await userRepository.GetByUserName(loginRequest.Username);
            if (existsUser == null)
            {
                Logger.LogWarning("User with username {0} doesn't exists", loginRequest.Username);
                return null;
            }

            if (!decryptor.PasswordDecrypt(existsUser.Password).Equals(loginRequest.Password))
            {
                Logger.LogWarning("Invalid password for user with username {0}", loginRequest.Username);
                return null;
            }

            Logger.LogInformation("User with username {0} successfully logged in", loginRequest.Username);

            return new LoginResponse
            {
                Token = tokenService.GenerateToken(existsUser)
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occurred");
            return null;
        }
    }
}