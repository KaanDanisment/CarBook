using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Helpers
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iteration = 100000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

        public static (byte[] hash, byte[] salt) HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iteration,Algorithm,HashSize);

            return (hash, salt);
        }

        public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iteration, Algorithm,HashSize);

            return inputHash.SequenceEqual(hash);
        }
    }
}
