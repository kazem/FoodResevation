using Common;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JwtService : IJwtService
    {
        private readonly SiteSettings _settings;
        public JwtService(IOptionsSnapshot<SiteSettings> settings)
        {
            _settings = settings.Value;
        }
        public string GenerateAsync(User user)
        {
            var secretkey = Encoding.UTF8.GetBytes(_settings.JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_settings.JwtSettings.EncryptKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = _getClaimsAsync(user);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Issuer = _settings.JwtSettings.Issuer,
                Audience = _settings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_settings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_settings.JwtSettings.ExpiresMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            var jwtToken = tokenHandler.WriteToken(securityToken);

            return jwtToken;
        }

        private IEnumerable<Claim> _getClaimsAsync(User user)
        {
            //var result = await signInManager.ClaimsFactory.CreateAsync(user);
            var list = new List<Claim>();
            list.Add(new Claim(ClaimTypes.MobilePhone, "09123456987"));
            list.Add(new Claim(new ClaimsIdentityOptions().SecurityStampClaimType, user.SecurityStamp.ToString()));
            return list;
        }
    }
}
