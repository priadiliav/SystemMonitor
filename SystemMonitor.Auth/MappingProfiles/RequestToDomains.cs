using AutoMapper;
using SystemMonitor.Models.Dtos.Request;
using SystemMonitor.Models.Entities;

namespace SystemMonitor.Auth.MappingProfiles;

public class RequestToDomains : Profile
{
   public RequestToDomains()
   {
      CreateMap<RegisterRequest, User>()
         .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Username))
         .ForMember(x => x.Password, opt => opt.MapFrom(x => x.Password))
         .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role));
   }
}