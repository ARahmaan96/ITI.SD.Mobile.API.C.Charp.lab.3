using Lab3.Context;
using Lab3.DTOs;
using Lab3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lab3.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        private readonly IConfiguration _configuration;
        public AccountServices(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string>? Login(LoginUserDTO userDTO)
        {
            if (userDTO.UserName == null || userDTO.Password == null) return null!;

            ApplicationUser? user = await _userManager.FindByNameAsync(userDTO.UserName);
            if (user == null) return null!;

            bool found = await _userManager.CheckPasswordAsync(user, userDTO.Password);
            if (!found) return null!;

            // Creating Claims
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            // Adding Rols to Claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));

            // Creating Key
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]!));

            // Creating Signing Credentials [First Part of the Token]
            SigningCredentials signCred = new(
                key,
                SecurityAlgorithms.HmacSha256
                );

            // Creating JW Token
            JwtSecurityToken token = new(
                issuer: _configuration["JWT:ValidateIssure"],
                audience: _configuration["JWT:ValidateAud"],
                claims: Claims,
                expires: DateTime.Now.AddYears(1),
                signingCredentials: signCred
                ); ;

            // return token in conpact repsentaion
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object>? Register(RegisterUserDTO userDTO)
        {
            ApplicationUser user = new ApplicationUser();
            user.Email = userDTO.Email;
            user.UserName = userDTO.UserName;

            IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password!);
            if (result.Succeeded)
            {
                // TODO: add rules
                return true;
            }
            return result.Errors.FirstOrDefault()!;
        }
    }
}
