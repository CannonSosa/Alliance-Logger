using System;

namespace api.Interfaces
{
    public interface IJwtService
    {
      /// <summary>Generates a Jwt Token.</summary>
      /// <exception cref="System.ArgumentException"></exception>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="SecurityTokenEncryptionFailedException"></exception>
      /// <param name="id">Used as the NameIdentifier in the Jwt Token.</param>
      /// <param name="userName">Used as the Sub and the Name in the Jwt Token.</param>
      /// <param name="audience">Identifies the receipients that the Jwt Token is intended for.</param>
      /// <param name="expires">When the Jwt Token expires.</param>
      /// <returns>A Jwt Token</returns>
      String BuildJwtToken(String id, String userName, String audience, DateTime expires, String userRoleNormalizedName = null);
    } 
}
