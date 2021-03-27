using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.HelpersAndExtensions
{
    using DeltaQrCode.ModelsDto;

    using Microsoft.AspNetCore.WebSockets.Internal;

    public static class Extensions
    {
        #region Encription
        public static string ToMd5(this string input)
        {
            return EncriptionHelpers.MD5Hash(input);
        }
        #endregion

        public static string ToStringPosition(this int input)
        {
            var position = ConstantsAndEnums.RowsDictionary.FirstOrDefault(x => x.Key == input);
            return position.Value;
        }
        public static string ToIntPosition(this string input)
        {
            var position = ConstantsAndEnums.RowsDictionary.FirstOrDefault(x => x.Value.ToUpper() == input.ToUpper());
            return position.Value;
        }
    }
}
