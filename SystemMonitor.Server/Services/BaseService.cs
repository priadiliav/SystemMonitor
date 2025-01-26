using AutoMapper;
using SystemMonitor.DataService.Contracts;

namespace SystemMonitor.Server.Services;

public class BaseService<T>(
    ILogger<T> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork)
{
    protected readonly ILogger<T> Logger = logger;
    protected readonly IMapper Mapper = mapper;
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;
}
