using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Sample_Authentication_ASP.Net_Core.Common
{
    public class PasswordHasher : IPasswordHasher
    {
        const int SaltSize = 128 / 8; // 128 bits

        public byte[] GenerateSalt()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];

            rng.GetBytes(salt);

            return salt;
        }

        public byte[] HashPassword(byte[] salt, string password)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits

            // Produce a version 2 text hash.
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);

            return outputBytes;
        }

        public bool IsPasswordVerified(byte[] salt, byte[] hashedPassword, string providedPassword)
        {
            var _hashedProvidedPass = HashPassword(salt, providedPassword);

            return _hashedProvidedPass.SequenceEqual(hashedPassword);
        }
    }
}
