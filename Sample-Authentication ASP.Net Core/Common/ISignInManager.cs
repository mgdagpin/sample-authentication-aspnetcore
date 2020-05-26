using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Authentication_ASP.Net_Core.Common
{
    public interface ISignInManager
    {
        Task PasswordSignInAsync(string usernameOrEmail, string password, bool? isPersistent);
        Task SignOutAsync();

    }
}
