using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class SecurityHelper
    {
=======
using System.Security.Cryptography;
using System.Text;

namespace SharedLibrary
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Mã hóa chuỗi bằng SHA-256
        /// </summary>
        public static string ComputeSHA256(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
>>>>>>> b31843c (SharedLibrary và TCPClient)
    }
}
