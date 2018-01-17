using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptSharp.Utility;
using System.Security.Cryptography;

namespace AalborgZooProjekt.Model
{
    class PasswordGenerator
    {
        public Tuple<string, string> HashPassword(string password) => HashPassword(password, "");

        public Tuple<string, string> HashPassword(string password, string salt)
        {
            if (salt == "")
                salt = Encoding.UTF8.GetString(generateSalt());

            var keyBytes = Encoding.UTF8.GetBytes(password);
            
            // The following code-block is just an assumption - might need tweaking.
            var cost = 262144;
            var blockSize = 8;
            var parallel = 1;
            var maxThreads = (int?)null;
            var derivedKeyLength = 128;
            // End of block to tweak.

            var bytes = SCrypt.ComputeDerivedKey(keyBytes, Encoding.UTF8.GetBytes(salt), cost, blockSize, parallel, maxThreads, derivedKeyLength);
            return new Tuple<string, string>(Convert.ToBase64String(bytes), salt);
        }

        private byte[] generateSalt() => generateSalt(50);

        private byte[] generateSalt(int maxLength)
        {
            byte[] salt = new byte[maxLength];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}
