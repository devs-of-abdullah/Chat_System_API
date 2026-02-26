
using DTOs;

namespace Business
{
    public interface IUserService
    {
        Task<int> RegisterAsync(RegisterUserDto user);
        Task<string> LoginAsync(LoginUserDto user);
        Task DeleteAsync(int id);
        Task UpdateAsync(int Id, UpdateUserDto userDto);
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto?> GetByEmailAsync(string email);
        Task<List<UserDto>> GetAllAsync();
    }
}
