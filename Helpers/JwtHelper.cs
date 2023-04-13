using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Forum.Helpers
{
	public class JwtHelper
	{
		private readonly IConfiguration _configuration;
		public JwtHelper(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// 创建一个JWT令牌
		/// </summary>
		/// <param name="StuNo">学号</param>
		/// <param name="Role">用户等级，参考Entities.Student注释</param>
		/// <param name="ExpiresIn">有效期（单位：分钟）</param>
		/// <returns>一个签发的JWT令牌</returns>
		public string CreateToken(string StuNo, string Role, int ExpiresIn)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, StuNo),
				new Claim(ClaimTypes.Role, Role),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				_configuration["Jwt:SecretKey"] ?? "SampleKey"));

			var algorithm = SecurityAlgorithms.HmacSha256;

			var signingCredentials = new SigningCredentials(secretKey, algorithm);

			var JwtSecurityToken = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"] ?? "SampleIssuer",
                audience: _configuration["Jwt:Audience"] ?? "SampleAudience",
                claims: claims,
				notBefore: DateTime.Now,
				expires: DateTime.Now.AddMinutes(ExpiresIn),
				signingCredentials: signingCredentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);

            return token;
		}
	}
}

