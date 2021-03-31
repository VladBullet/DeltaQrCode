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
            var str = input.Split(",");
            var rand = str[0].Split(":")[1];
            var pozitie = str[1].Split(":")[1];
            var interval = str[2].Split(":")[1];

            return new Position(rand, pozitie, interval);

        }

        public static string ToJson(this Dimensiuni dim)
        {
            return "{ "+ dim.ToCustomString() + " }";
        }
        public static string ToCustomString(this Dimensiuni dim)
        {
            if (dim != null)
            {
                return string.Format("Diam:{0}, Lat:{1}, H:{2}", dim.Diam, dim.Lat, dim.H);
            }

            return string.Empty;
        }
        public static Dimensiuni ToDimensiuniFromJsonString(this string str)
        {
            var result = JsonConvert.DeserializeObject<Dimensiuni>(str);
            if (result == null)
            {
                result = new Dimensiuni("0", "0", "0");
            }
            return result;
        }


        public static string ToJson(this Uzura uz)
        {
            return "{ " + uz.ToCustomString() + " }";
        }
        public static string ToCustomString(this Uzura uz)
        {
            if (uz != null)
            {
                return string.Format("StF:{0}, DrF:{1}, StS:{2}, DrS:{3}", uz.StF, uz.DrF, uz.StS, uz.DrS);
            }

            return string.Empty;
        }
        public static Uzura ToUzuraFromJsonString(this string str)
        {
            var result = JsonConvert.DeserializeObject<Uzura>(str);
            if (result == null)
            {
                result = new Uzura("nan", "nan", "nan", "nan");
            }
            return result;
        }

        public static string ToJson(this string input)
        {
            return JsonConvert.SerializeObject(input);
        }

    }
}
