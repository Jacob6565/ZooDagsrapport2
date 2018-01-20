using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class PasswordChecker
    {
        public bool DoesPasswordsMatch(string storedPasswordHash, string storedSalt, string givenPasswordPlain)
        {
            PasswordGenerator generator = new PasswordGenerator();
            string givenPasswordHash = generator.HashPassword(givenPasswordPlain, storedSalt).Item1;

            byte[] storedPassword = Encoding.UTF8.GetBytes(storedPasswordHash);
            byte[] givenPassword = Encoding.UTF8.GetBytes(givenPasswordHash);

            int maxLength = storedPassword.Length > givenPassword.Length ? storedPassword.Length : givenPassword.Length;

            bool hasMiss = false;
            for (int i = 0; i < maxLength; i++)
            {
                if (givenPassword[i] != storedPassword[i])
                    hasMiss = true;
            }

            return hasMiss;
        }
    }
}
