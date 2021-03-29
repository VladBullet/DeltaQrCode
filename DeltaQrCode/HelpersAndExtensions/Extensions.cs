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
            return string.Join("{", dim.ToCustomString(), "}");
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
            return result;
        }


        public static string ToJson(this Uzura uz)
        {
            return string.Join("{", uz.ToCustomString(), "}");
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
            return result;
        }

        public static string ToJson(this string input)
        {
            return string.Concat("{", input, "}");
        }

        public static Dimensiuni FromStringToDimensiuni(this string input)
        {
            var json = input.ToJson();
            var model = json.ToDimensiuniFromJsonString();
            return model;
        }

    }
}
