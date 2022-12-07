using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Projekt_ASP.Service
{
    public class PasswordEncryption
    {
        private static byte[] SALT = RandomNumberGenerator.GetBytes(128 / 8);
        private static int ITERATION_COUNT = 100000;

        public static string Hash(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: SALT,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: ITERATION_COUNT,
                numBytesRequested: 512 / 8
                ));

            return hashed;

        }

    }
}