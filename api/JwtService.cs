using api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api
{
  public class JwtService : IJwtService
  {
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    /// <summary>Generates a Jwt Token.</summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="SecurityTokenEncryptionFailedException"></exception>
    /// <param name="id">Used as the NameIdentifier in the Jwt Token.</param>
    /// <param name="userName">Used as the Sub and the Name in the Jwt Token.</param>
    /// <param name="audience">Identifies the receipients that the Jwt Token is intended for.</param>
    /// <param name="expires">When the Jwt Token expires.</param>
    /// <returns>A Jwt Token</returns>
    public string BuildJwtToken(string id, string userName, string audience, DateTime expires, String userRoleNormalizedName = null)
    {
      #region Argument Exceptions

      if (String.IsNullOrWhiteSpace(id))
      {
        throw new ArgumentNullException("id");
      }

      if (String.IsNullOrWhiteSpace(userName))
      {
        throw new ArgumentNullException("userName");
      }

      if (String.IsNullOrWhiteSpace(audience))
      {
        throw new ArgumentNullException("audience");
      }

      if (expires == null)
      {
        throw new ArgumentNullException("expires");
      }

      if (_configuration.GetValue<byte[]>("EncryptionKeys:AesKey") == null)
      {
        throw new ArgumentNullException("AllianceApp.EncryptionKeys.AesKey", "The AesKey was not located.");
      }

      #endregion

      SymmetricSecurityKey key = new SymmetricSecurityKey(_configuration.GetValue<byte[]>("EncryptionKeys:AesKey"));
      IdentityOptions options = new IdentityOptions();
      List<Claim> claims = new List<Claim>()
      {
        new Claim(JwtRegisteredClaimNames.Sub, userName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(options.ClaimsIdentity.UserIdClaimType, id),
        new Claim(options.ClaimsIdentity.UserNameClaimType, userName)
      };

      if (!String.IsNullOrWhiteSpace(userRoleNormalizedName))
      {
        claims.Add(new Claim(options.ClaimsIdentity.RoleClaimType, userRoleNormalizedName));
      }
     
      JwtSecurityToken jwt = new JwtSecurityToken(        
        audience: audience,
        claims: claims,
        notBefore: DateTime.UtcNow,
        expires: expires,
        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
      );

      string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
      if (!String.IsNullOrWhiteSpace(encodedJwt))
      {
        return encodedJwt;
      }
      else
      {
        throw new SecurityTokenEncryptionFailedException($"The jwt could not be serialized for {userName}.");
      }
    }
  }
}
