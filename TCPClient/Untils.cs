using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TCPClient
{
    public static class Utils
    {
        public static string Sha256Hash(string input)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input ?? "");
                byte[] hash = sha.ComputeHash(bytes);
                var sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
