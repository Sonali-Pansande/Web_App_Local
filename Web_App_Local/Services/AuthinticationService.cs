using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web_App_Local.Models;

namespace Web_App_Local.Services
{
    public class CorAuthService
    {
        IConfiguration configuration;
        SignInManager<IdentityUser> signInManager;
        UserManager<IdentityUser> userManager;

        public CorAuthService(IConfiguration configuration, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;

        }

      public async Task<bool> RegisterUserAsync(RegisterUser register)
        {
            bool Iscreated = false;
            var registeruser = new IdentityUser() { UserName = register.EMail ,Email = register.EMail};
            var result = await userManager.CreateAsync(registeruser, register.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return Iscreated;
        }

        public async Task<string> AuthinticateUserAsync(LoginUser inputmodel)
        {
            string jwtToken = "";

            var result = await signInManager.PasswordSignInAsync(inputmodel.UserName, inputmodel.Password,false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var secretKey = Convert.FromBase64String(configuration["JWTCoreSettings:SecretKey"]);
                var expiryTimeSpan = Convert.ToInt32(configuration["JWTCoreSettings:ExpiryInMinuts"]);

                IdentityUser user = new IdentityUser(inputmodel.UserName);

                var securityTokenDescription = new SecurityTokenDescriptor()
                {
                    Issuer = null,
                    Audience = null,
                    Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("username", user.Id, ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(expiryTimeSpan),
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var jwtHandler = new JwtSecurityTokenHandler();
                var jwToken = jwtHandler.CreateJwtSecurityToken(securityTokenDescription);
                jwtToken = jwtHandler.WriteToken(jwToken);
            }
            else
            {
                jwtToken = "Login Failed";
            }
            return jwtToken;
          }
    }
}
