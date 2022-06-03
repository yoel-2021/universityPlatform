using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace universityPlatform.TokenCreation
{
    public class VerificarTokenGen
    {
        public string? validartokengen(string Token, JwtSettings _jwtSettings)
        {
           
            if (Token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_jwtSettings.IssuerSigningKey);
            try
            {
                tokenHandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    LifetimeValidator = this.LifetimeValidator,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero}, out SecurityToken validatedToken) ;

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.First(x => x.Type == "Name").Value;

                // return user id from JWT token if validation successful
                return userName;

            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        private bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            var valid = false;

            if ((expires.HasValue && DateTime.UtcNow < expires)
                && (notBefore.HasValue && DateTime.UtcNow > notBefore))
            { valid = true; }

            return valid;
        }
    }
}
