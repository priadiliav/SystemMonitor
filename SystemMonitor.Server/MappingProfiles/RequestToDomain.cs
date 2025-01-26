using AutoMapper;
using SystemMonitor.Models.Dtos;
using SystemMonitor.Models.Entities;

namespace SystemMonitor.Server.MappingProfiles;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
        CreateMap<ComputerDetailsDto, ComputerDetails>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Metrics, opt => opt.MapFrom(src => src.Metrics));

        CreateMap<ComputerMetricsDto, ComputerMetrics>()
            .ForMember(dest => dest.CpuUsage, opt => opt.MapFrom(src => src.CpuUsage))
            .ForMember(dest => dest.NetworkUsage, opt => opt.MapFrom(src => src.NetworkUsage))
            .ForMember(dest => dest.DiskUsage, opt => opt.MapFrom(src => src.DiskUsage))
            .ForMember(dest => dest.RamUsage, opt => opt.MapFrom(src => src.RamUsage));
    }
}
