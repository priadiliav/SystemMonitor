﻿using Microsoft.Extensions.Logging;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.DataService.Data;

namespace SystemMonitor.DataService.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    public IComputerDetailsRepository ComputerDetails { get; set; }
    public IComputerMetricsRepository ComputerMetrics { get; set; }
    public IUserRepository Users { get; set; }

    public UnitOfWork(
        AppDbContext appDbContext, 
        ILogger<ComputerDetailsRepository> detailsLogger, 
        ILogger<ComputerMetricsRepository> metricsLogger,
        ILogger<UserRepository> userLogger)
    {
        _context = appDbContext;

        ComputerDetails = new ComputerDetailsRepository(_context, detailsLogger);
        ComputerMetrics = new ComputerMetricsRepository(_context, metricsLogger);
        Users = new UserRepository(_context, userLogger); 
    }

    public async Task<bool> CompleteTask()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
