using AutoMapper;
using Cybersecurity.Entities;
using Cybersecurity.Models.DTO.Log;

namespace Cybersecurity.Mappings
{
    public class LogMappingProfile : Profile
    {
        public LogMappingProfile()
        {
            CreateMap<Log, LogDto>()
                .ForMember(u => u.UserName, c => c.MapFrom(r => r.User.Login));
        }
    }
}
