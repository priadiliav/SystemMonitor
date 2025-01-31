using AutoMapper;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.Models.Dtos;
using SystemMonitor.Models.Entities;

namespace SystemMonitor.Server.Services;

public class ComputerService(
    IComputerDetailsRepository computerDetailsRepository,
    IComputerMetricsRepository computerMetricsRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ILogger<ComputerService> logger)
    : BaseService<ComputerService>(logger, mapper, unitOfWork)
{
    public async Task<List<ComputerDetails>> Get()
    {
        try
        {
            var computerDetails = await computerDetailsRepository.Get();
            return computerDetails.ToList();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occurred");
            throw;
        }
    }

    public async Task AddOrUpdate(ComputerDetailsDto computerDetailsDto)
    {
        if (computerDetailsDto == null)
            throw new ArgumentNullException(nameof(computerDetailsDto), "ComputersDto can't be null");

        try
        {
            var existingComputer = await computerDetailsRepository.GetByName(computerDetailsDto.Name);

            if (existingComputer != null)
            {
                var existingMetrics =
                    await computerMetricsRepository.GetComputerMetricsByComputerId(existingComputer.Id);
                if (existingMetrics != null)
                {
                    Mapper.Map(computerDetailsDto.Metrics, existingMetrics);
                    existingMetrics.ComputerDetailsId = existingComputer.Id;
                    existingComputer.ComputerMetricsId = existingMetrics.Id;

                    await computerMetricsRepository.Update(existingMetrics);
                    await computerDetailsRepository.Update(existingComputer);
                }
                else
                {
                    var newMetrics = Mapper.Map<ComputerMetrics>(computerDetailsDto.Metrics);
                    newMetrics.ComputerDetailsId = existingComputer.Id;
                    existingComputer.ComputerMetricsId = newMetrics.Id;

                    await computerMetricsRepository.Add(newMetrics);
                    await computerDetailsRepository.Update(existingComputer);
                }

                await UnitOfWork.CompleteTask();
            }
            else
            {
                var newComputer = Mapper.Map<ComputerDetails>(computerDetailsDto);
                var newMetrics = Mapper.Map<ComputerMetrics>(computerDetailsDto.Metrics);
                newMetrics.ComputerDetailsId = newComputer.Id;
                newComputer.ComputerMetricsId = newMetrics.Id;

                await computerDetailsRepository.Add(newComputer);
                await computerMetricsRepository.Add(newMetrics);

                await UnitOfWork.CompleteTask();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occurred");
            throw;
        }
    }
}