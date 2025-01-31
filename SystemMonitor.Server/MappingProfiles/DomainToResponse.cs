using AutoMapper;
using SystemMonitor.Models.Dtos.Response;
using SystemMonitor.Models.Entities;

namespace SystemMonitor.Server.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<User, RegisterResponse>()
            .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Username))
            .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role));

        CreateMap<User, LoginResponse>()
            .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Username))
            .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role))
            .ForMember(x => x.Token, opt => opt.Ignore());
    }
}