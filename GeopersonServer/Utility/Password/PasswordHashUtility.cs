using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Utility.Password
{
    public static class PasswordHashUtility
    {
        public static string GenerateHashPassword(string password) {
            var hasher = new PasswordHasher();
            return hasher.HashPassword(password);
        }
        public static PasswordVerificationResult ComparePassword(string hashedPassword, string providedPassword) {
            var hasher = new PasswordHasher();
            return hasher.VerifyHashedPassword(hashedPassword, providedPassword);
        }

    }
}
