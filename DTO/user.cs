
using System.ComponentModel.DataAnnotations;

namespace DTOs
{


    public record CreateUserDTO
    {
        [Required, MaxLength(50)]
        public string Username { get; init; } = null!;

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; init; } = null!;
        [Required, MinLength(6)]
        public string Password { get; init; } = null!;
        [Required]
        public string Role { get; init; } = null!;
    }

    public record ReadUserDTO
    {
        public int Id { get; init; }
        public string Email { get; init; } = null!;
        public string Role { get; init; } = null!;
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
    public record UpdateUserPasswordDTO
    {
        [Required]
        public string CurrentPassword { get; init; } = null!;
        [Required, MinLength(6)]
        public string NewPassword { get; init; } = null!;
    }
    public class UpdateUserDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public record UpdateUserEmailDTO
    {
        [Required]
        public int Id { get; init; }
        [Required, EmailAddress, MaxLength(256)]
        public string NewEmail { get; init; } = null!;
        [Required, MinLength(6)]
        public string CurrentPassword { get; init; } = null!;
    }
    public record SoftUserDeleteDTO
    {
        [Required, MinLength(6)]
        public string CurrentPassword { get; init; } = null!;
    }
    public record UpdateUserRoleDTO
    {
        [Required]
        public string NewRole { get; init; } = null!;
    }
}
