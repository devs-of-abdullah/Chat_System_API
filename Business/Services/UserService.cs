using Business.Interfaces;
using Data;
using DTOs;
using Entities;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> CreateAsync(CreateUserDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                throw new InvalidOperationException("Password must be at least 6 characters.");

            var normalizedEmail = dto.Email.Trim().ToLower();

            if (await _repo.ExistsByEmailAsync(normalizedEmail))
                throw new InvalidOperationException($"'{normalizedEmail}' email already exists");

            var user = new UserEntity
            {
                Username = dto.Username,
                Email = normalizedEmail,
                Role = dto.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12),
                CreatedAt = DateTime.UtcNow
            };

            return await _repo.CreateAsync(user);
        }

        public async Task<ReadUserDTO?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;

            return MapToReadDTO(user);
        }

        public async Task<ReadUserDTO?> GetByEmailAsync(string email)
        {
            var normalizedEmail = email.Trim().ToLower();
            var user = await _repo.GetByEmailAsync(normalizedEmail);

            if (user == null) return null;

            return MapToReadDTO(user);
        }

        public async Task SoftDeleteAsync(int id, SoftUserDeleteDTO dto)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            VerifyPassword(dto.CurrentPassword, user.PasswordHash);

            if (user.IsDeleted)
                throw new InvalidOperationException("User already deleted");

            user.IsDeleted = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(user);
        }

        public async Task UpdatePasswordAsync(int userId, UpdateUserPasswordDTO dto)
        {
            var user = await _repo.GetByIdAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            VerifyPassword(dto.CurrentPassword, user.PasswordHash);

            if (dto.NewPassword.Length < 6)
                throw new InvalidOperationException("Password must be at least 6 characters.");

            if (BCrypt.Net.BCrypt.Verify(dto.NewPassword, user.PasswordHash))
                throw new InvalidOperationException("New password cannot be same as old password");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword, workFactor: 12);
            user.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(user);
        }

        public async Task UpdateRoleAsync(int id, UpdateUserRoleDTO dto)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.Role = dto.NewRole;
            user.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(user);
        }

        public async Task UpdateEmailAsync(int userId, UpdateUserEmailDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NewEmail))
                throw new ArgumentException("Email cannot be empty");

            var normalizedEmail = dto.NewEmail.Trim().ToLower();

            var user = await _repo.GetByIdAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            if (user.IsDeleted)
                throw new InvalidOperationException("User account is deleted");

            VerifyPassword(dto.CurrentPassword, user.PasswordHash);

            if (user.Email == normalizedEmail)
                throw new InvalidOperationException("New email cannot be same as current email");

            var existing = await _repo.GetByEmailAsync(normalizedEmail);

            if (existing != null && existing.Id != userId)
                throw new InvalidOperationException("Email already in use");

            user.Email = normalizedEmail;
            user.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(user);
        }

        private static void VerifyPassword(string inputPassword, string storedHash)
        {
            if (!BCrypt.Net.BCrypt.Verify(inputPassword, storedHash))
                throw new UnauthorizedAccessException("Password is incorrect");
        }

        private static ReadUserDTO MapToReadDTO(UserEntity user)
        {
            return new ReadUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}