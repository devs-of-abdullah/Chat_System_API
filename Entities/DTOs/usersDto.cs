
namespace Entities.DTOs
{

    public class RegisterDto
    {
        public string Fullname{ get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginDto
    {
        public string Email { get; set; }  = null!;
        public string Password { get; set; } = null!;
    }
   


}
