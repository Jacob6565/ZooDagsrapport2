using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class PasswordChecker
    {
        public bool DoesPasswordsMatch(string storedPasswordHash, string storedSalt, string givenPassword)
        {
            PasswordGenerator generator = new PasswordGenerator();
            string givenPasswordHash = generator.HashPassword(givenPassword, storedSalt).Item1;

            if (givenPasswordHash == storedPasswordHash)
                return true;
            else
                return false;
        }
    }
}
