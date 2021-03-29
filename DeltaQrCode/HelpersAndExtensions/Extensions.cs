using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.HelpersAndExtensions
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    using Microsoft.AspNetCore.WebSockets.Internal;

    using Newtonsoft.Json;

    public static class Extensions
    {
        #region Encription
        public static string ToMd5(this string input)
        {
            return EncriptionHelpers.MD5Hash(input);
        }
        #endregion

        public static Position ToPosition(this string input)
        {
            var rand = input[0].ToString();
            var pozitie = input[1].ToString();

            return new Position(rand, pozitie);

        }

        public static string ToJson(this Dimensiuni dim)
        {
            return string.Join("{", dim.ToString(), "}");
        }
        public static string ToString(this Dimensiuni dim)
        {
            return  string.Format("Diam:{0}, Lat:{1}, H:{2}", dim.Diametru, dim.Latime, dim.Inaltime);
        } 
        public static Dimensiuni ToDimensiuniFromJsonString(this string str)
        {
            var result = JsonConvert.DeserializeObject<Dimensiuni>(str);
            return null;
        }

    }
}
