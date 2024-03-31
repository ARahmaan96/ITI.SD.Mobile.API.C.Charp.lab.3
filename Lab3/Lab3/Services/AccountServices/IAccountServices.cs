using Lab3.DTOs;

namespace Lab3.Services.AccountServices
{
    public interface IAccountServices
    {
        public Task<object>? Register(RegisterUserDTO userDTO);
        public Task<string>? Login(LoginUserDTO userDTO);
    }
}
