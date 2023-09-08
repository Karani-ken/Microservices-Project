using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Registration_System.Models;
using Registration_System.Services.IServices;
using Registration_System.Utility;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Registration_System.Services
{
    public class JwtService : IJwtInterface
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            //Signing Credentials
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //payload-data
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
