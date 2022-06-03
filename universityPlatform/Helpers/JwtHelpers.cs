using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using universityPlatform.TokenCreation;
using System.Net;
using System.Text;

namespace universityPlatform.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.Email),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                //new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(120).ToString("HH:mm: ss tt")) //.ToString("MMM ddd dd yyyy HH:mm: ss tt"))

        };
            if (userAccounts.role == "Administrator")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else if (userAccounts.role == "User")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));
            }
            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }
        //Para generar token
        public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserTokens();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                // obtain SECRET KEY
                var Key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;

                //Expires in 2 horas
                DateTime expireTime = DateTime.UtcNow.AddMinutes(120);

                //validity of our Token 
                UserToken.Validity = expireTime.TimeOfDay;

                //Generate our Jwt
                var jwToken = new JwtSecurityToken(

                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).LocalDateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Key),
                        SecurityAlgorithms.HmacSha256));


                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                UserToken.UserName = model.UserName;
                UserToken.Id = model.Id;
                UserToken.GuidId = Id;
                UserToken.role = model.role;
                return UserToken;

            }
            catch (Exception exception)
            {
                throw new Exception("Error Generating the JWT", exception);
            }
        }
    }
   
}



