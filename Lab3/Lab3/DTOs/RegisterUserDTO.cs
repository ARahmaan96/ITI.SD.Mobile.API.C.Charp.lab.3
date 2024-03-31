using Lab3.Models;
using System.ComponentModel.DataAnnotations;

namespace Lab3.DTOs
{
    public class RegisterUserDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public RegisterUserDTO() { }
        public RegisterUserDTO(ApplicationUser user)
        {
            UserName = user.UserName;
            Email = user.Email;
            Password = user.PasswordHash;
        }
    }
}
