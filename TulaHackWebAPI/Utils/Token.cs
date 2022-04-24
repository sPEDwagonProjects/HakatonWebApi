using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TulaHackWebAPI.Etc;
using TulaHackWebAPI.Model;

namespace TulaHackWebAPI.Utils
{
	public static class Token
	{
		public const string Key = "jXEIIrquioTfLCzq";
		public static TimeSpan LifeTime = TimeSpan.FromDays(1);
		public const string AUDIENCE = "MyAuthClient";
		public const string ISSUER = "MyAuthServer";

		//Генерация токена
		public static  (string access_token, string user_name, int roleid, int expires_in) GenerateToken(User user)
        {
			

			var now = DateTime.UtcNow;
			var claims = new List<Claim>
					{
						new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
						new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),

						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
					};
			ClaimsIdentity identity =
			new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);

		

			var jwt = new JwtSecurityToken
			(
					issuer: ISSUER,
					audience:AUDIENCE,
					claims: identity.Claims,
					notBefore: now,
					expires: now.Add(LifeTime),
					signingCredentials: new SigningCredentials(Generators.GetSymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256)
			);



			var res = (new JwtSecurityTokenHandler().WriteToken(jwt), user.Login, user.RoleId, (int)LifeTime.TotalSeconds);
			
			return res;


		}
		//Получение ClaimsEdentity
		
		//Получение параметров токена
		public static TokenValidationParameters GetTokenValidationParameters()
		{
			return new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidIssuer = ISSUER,
				ValidAudience = AUDIENCE,
				ValidateLifetime=true,
				IssuerSigningKey = Generators.GetSymmetricSecurityKey(Key),
			};
		}
		
		
			
		
	}
}
