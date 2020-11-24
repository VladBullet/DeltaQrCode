using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    public static class Extensions
    {
        public static string ToMd5(this string input)
        {
            return Helpers.MD5Hash(input);
        }
    }
}
