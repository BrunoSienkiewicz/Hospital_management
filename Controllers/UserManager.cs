using Azure;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.Dto;
using Hospital_Management.Models.ViewModels.Appointment;
using Hospital_Management.Repositiory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital_Management.Controllers
{
	public class UserManager
	{
		private readonly UserRepository userRepository;
		private readonly JwtConfig jwtConfig;
		public UserManager(IUserRepository userRepository, IConfiguration configuration)
		{
			this.userRepository = (UserRepository?)userRepository;
			jwtConfig = configuration.GetSection("Jwt").Get<JwtConfig>();
		}

		public async Task<bool> CheckPassword(UserDto userDto, string password)
		{
			var user = await userRepository.GetEntityById(userDto.UserId);
			if (user == null)
			{
				return false;
			}

			return user.Password == password;
		}

		public async Task<IList<string>> GetRoles(UserDto userDto)
		{
			var user = await userRepository.GetEntityById(userDto.UserId);

			return new List<string> { user.UserType };
		}

		public async Task<UserDto> FindByName(string username)
		{
			var user = await userRepository.GetByUsername(username);
			if (user == null)
			{
				return null;
			}

			return new UserDto
			{
				UserId = user.UserId,
				Username = user.Username,
				UserType = user.UserType
			};
		}

		public async Task<string> CreateToken(UserDto user)
		{
			var roles = await GetRoles(user);
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, string.Join(",", roles))
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: jwtConfig.Issuer,
				audience: jwtConfig.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddHours(1),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
		{
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Key);
            var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            return principal;
        }
	}
}
