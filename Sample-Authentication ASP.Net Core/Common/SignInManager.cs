using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sample_Authentication_ASP.Net_Core.Common
{
    public class SignInManager : ISignInManager
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger logger;
        private readonly IPasswordHasher passwordHasher;

        public SignInManager(
            IHttpContextAccessor contextAccessor,
            ILogger<SignInManager> logger,
            IPasswordHasher passwordHasher)
        {
            this.contextAccessor = contextAccessor;
            this.logger = logger;
            this.passwordHasher = passwordHasher;
        }

        public async Task PasswordSignInAsync(string usernameOrEmail, string password, bool? isPersistent)
        {
            AppUser _user = new AppUser
            {
                ID = 1,
                Name = "Vince",
                Roles = new[] { "Admin" } // this role can be configured here Startup.ConfigureServices (Startup.cs)
            };

            string _userData = JsonConvert.SerializeObject(_user);
            Guid _sessionUID = Guid.NewGuid();

            var _claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, _user.ID.ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypes.Name, _user.Name, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Sid, _sessionUID.ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypes.UserData, _userData, ClaimValueTypes.String)
                };

            foreach (var _role in _user.Roles)
            {
                _claims.Add(new Claim(ClaimTypes.Role, _role, ClaimValueTypes.String));
            }

            var _identity = new ClaimsIdentity(_claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var _principal = new ClaimsPrincipal(_identity);

            await contextAccessor.HttpContext.SignInAsync(_principal, new AuthenticationProperties
            {
                IsPersistent = isPersistent == true
            });
        }

        public async Task SignOutAsync()
        {
            await contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
