using Lab3.Models;
using System.ComponentModel.DataAnnotations;

namespace Lab3.DTOs
{
    public class LoginUserDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public LoginUserDTO() { }
        public LoginUserDTO(ApplicationUser user)
        {
            UserName = user.UserName;
            Password = user.PasswordHash;
        }
    }
}
