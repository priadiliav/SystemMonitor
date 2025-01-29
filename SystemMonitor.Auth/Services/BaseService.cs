using AutoMapper;
using SystemMonitor.DataService.Contracts;

namespace SystemMonitor.Auth.Services;

public class BaseService<T>(
    ILogger<T> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork)
{
    protected readonly ILogger<T> Logger = logger;
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;
    protected readonly IMapper Mapper = mapper;
}
