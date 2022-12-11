using Cybersecurity.Models.DTO;

namespace Cybersecurity.Interfaces.Services
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto registerDto);
        Task<LoginResponseDto> LoginUser(LoginUserDto loginDto);
        Task Logout();
        Task UpdateUser(int id, UpdateUserDto updateDto);
        Task ChangePassword(ChangePasswordDto changePasswordDto);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(int id);
        Task<IEnumerable<RoleDto>> GetRoles();
        Task<int> GetRole();
        Task DeleteUser(int id);
        Task<string> SetOneTimePassword(int id);
    }
}
