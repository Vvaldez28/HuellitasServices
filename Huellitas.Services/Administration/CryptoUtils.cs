using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Services.Administration
{
    public static class CryptoUtils
    {
        public static string HashMD5(this byte[] data)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(data);
            return FormatUpperHex(hash);
        }

        public static string HashSHA256(this string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] hash = SHA256.Create().ComputeHash(data);
            return FormatHex(hash);
        }

        private static string FormatHex(byte[] data)
        {
            var builder = new StringBuilder();
            foreach (var b in data)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        private static string FormatUpperHex(byte[] data)
        {
            var builder = new StringBuilder();
            foreach (var b in data)
            {
                builder.Append(b.ToString("X2"));
            }
            return builder.ToString();
        }

        public static string getHashPassword(string variable)
        {
            return CryptoUtils.HashSHA256(variable).Substring(0, 10);
        }

        public static string getHashKey(string variable)
        {
            return CryptoUtils.HashSHA256(variable).Substring(0, 4);
        }
    }
}
