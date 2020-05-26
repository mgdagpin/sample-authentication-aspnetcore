using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Authentication_ASP.Net_Core.Common
{
    public interface IPasswordHasher
    {
        byte[] GenerateSalt();

        byte[] HashPassword(byte[] salt, string password);

        bool IsPasswordVerified(byte[] salt, byte[] hashedPassword, string password);
    }
}
