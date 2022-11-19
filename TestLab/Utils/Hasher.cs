using System.Security.Cryptography;
using System;

namespace TestLab.Utils
{
    public sealed class Hasher
    {
        private const int SALT_SIZE = 16;
        private const int HASH_SIZE = 20;
        private const string MARKER = "$H@#";

        public string Hash(string password, int iterations)
        {
            //create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SALT_SIZE]);

            //create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HASH_SIZE);

            //combine salt and hash
            var hashBytes = new byte[SALT_SIZE + HASH_SIZE];
            Array.Copy(salt, 0, hashBytes, 0, SALT_SIZE);
            Array.Copy(hash, 0, hashBytes, SALT_SIZE, HASH_SIZE);

            //convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            //format hash with extra information
            return string.Format($"{MARKER}{iterations}${base64Hash}");
        }

        public string Hash(string password)
        {
            return Hash(password, 10000);
        }

        public bool IsHashSupported(string hashString)
        {
            return hashString.Contains(MARKER);
        }

        public bool Verify(string password, string hashedPassword)
        {
            try
            {
                //check hash
                if (IsHashSupported(hashedPassword) == false)
                {
                    return false;
                }

                //extract iteration and Base64 string
                var splittedHashString = hashedPassword.Replace(MARKER, "").Split('$');
                var iterations = int.Parse(splittedHashString[0]);
                var base64Hash = splittedHashString[1];

                //get hashbytes
                var hashBytes = Convert.FromBase64String(base64Hash);

                //get salt
                var salt = new byte[SALT_SIZE];
                Array.Copy(hashBytes, 0, salt, 0, SALT_SIZE);

                //create hash with given salt
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
                byte[] hash = pbkdf2.GetBytes(HASH_SIZE);

                //get result
                for (var i = 0; i < HASH_SIZE; i++)
                {
                    if (hashBytes[i + SALT_SIZE] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

