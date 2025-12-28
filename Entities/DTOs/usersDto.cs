
namespace Entities.DTOs
{
    public class RegisterUserInControllerDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class RegisterUserDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }


    public class LoginUserDto
    {
        public string Email { get; set; }  = null!;
        public string Password { get; set; } = null!;
    }
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }


}
