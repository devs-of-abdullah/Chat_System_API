using Entities.DTOs;

namespace Business
{
    public interface IUserService
    {
        Task<RegisterDto> RegisterUserAsync(string name, string email, string password);
        Task<LoginDto> LoginUserAsync(string email, string password);

    }
}
