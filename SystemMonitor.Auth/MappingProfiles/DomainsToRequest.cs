using AutoMapper;
using SystemMonitor.Models.Dtos.Response;
using SystemMonitor.Models.Entities;

namespace SystemMonitor.Auth.MappingProfiles;

public class DomainsToRequest : Profile
{
    public DomainsToRequest()
    {
        CreateMap<User, RegisterResponse>()
            .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Username))
            .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role));
    }
}