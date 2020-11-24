using DeltaQrCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    using System.Security.Cryptography;
    using System.Text;

    public static class Helpers
    {
        public static string MD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
    public static class GuidHelper
    {
        public static Guid GetGuid(string guidString)
        {
            Guid returnValue;
            Guid.TryParse(guidString, out returnValue);
            return returnValue;
        }
    }
    public static class EnumHelper
    {
        public static string ToDisplayString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff) ?? "";
        }

        public static EnumType ToEnumValue<EnumType>(this String enumValue)
        {
            return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
        }
    }
}
