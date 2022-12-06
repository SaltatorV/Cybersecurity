using Cybersecurity.Models.DTO;

namespace Cybersecurity.Interfaces.Services
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto registerDto);
        Task<UserDto> LoginUser(LoginUserDto loginDto);
        Task UpdateUser(int id, UpdateUserDto updateDto);
        Task AdminUpdateUser(int id, AdminUpdateUserDto adminUpdateDto);
        Task ChangePassword(int id, ChangePasswordDto changePasswordDto);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(int id);
        Task<AdminUserDto> AdminGetUser(int id);
        Task<IEnumerable<RoleDto>> GetRoles();

        Task DeleteUser(int id);
    }
}
