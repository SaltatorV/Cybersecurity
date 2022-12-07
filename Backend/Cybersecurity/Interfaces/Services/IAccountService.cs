using Cybersecurity.Models.DTO;

namespace Cybersecurity.Interfaces.Services
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto registerDto, string userChangerId);
        Task<UserDto> LoginUser(LoginUserDto loginDto);
        Task UpdateUser(int id, UpdateUserDto updateDto);
        Task ChangePassword(int id, ChangePasswordDto changePasswordDto);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(int id);
        Task<IEnumerable<RoleDto>> GetRoles();

        Task DeleteUser(int id);
    }
}
