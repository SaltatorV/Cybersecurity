using AutoMapper;
using Cybersecurity.Entities;
using Cybersecurity.Models.DTO;

namespace Cybersecurity.Mappings
{
    public class AccountMappingProfile : Profile 
    {
        public AccountMappingProfile()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<ChangePasswordDto, User>();
            CreateMap<LoginUserDto, User>();

            CreateMap<User, UserDto>()
                .ForMember(u => u.RoleName, c => c.MapFrom(r => r.Role.Name));

            CreateMap<Role, RoleDto>();

            CreateMap<ChangePasswordDto, OldPassword>()
                .ForMember(o => o.Password, g => g.MapFrom(c => c.OldPassword));
        }
    }
}
