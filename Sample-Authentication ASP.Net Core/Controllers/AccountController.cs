using Microsoft.AspNetCore.Mvc;
using Sample_Authentication_ASP.Net_Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Authentication_ASP.Net_Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISignInManager signInManager;

        public AccountController(ISignInManager signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]        
        public async Task<IActionResult> LoginSubmit()
        {
            await signInManager.PasswordSignInAsync("test", "test", false);

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Redirect("/");
        }
    }
}
