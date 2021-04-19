﻿namespace DeltaQrCode.HelpersAndExtensions
{
    using System.Security.Policy;

    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

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
            return "{ " + dim.ToCustomString() + " }";
        }
        public static string ToCustomString(this Dimensiuni dim)
        {
            if (dim != null)
            {
                return string.Format("Diam:{0}, Lat:{1}, H:{2}, Dot:{3}", dim.Diam, dim.Lat, dim.H, dim.Dot);
            }

            return string.Empty;
        }

        public static string ToDisplayString(this Dimensiuni dim)
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
                result = new Dimensiuni(0, 0, 0, 0);
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
                return string.Format("StF:{0}, DrF:{1}, StS:{2}, DrS:{3}", uz.StF, uz.DrF.HasValue ? uz.DrF.Value.ToString() : "null", uz.StS.HasValue ? uz.StS.Value.ToString() : "null", uz.DrS.HasValue ? uz.DrS.Value.ToString() : "null");
            }

            return string.Empty;
        }

        public static string ToDisplayString(this Uzura uz)
        {
            if (uz != null)
            {
                return string.Format("StF:{0}, DrF:{1}", uz.StF, uz.DrF.HasValue ? uz.DrF.Value.ToString() : "null");
            }
            return string.Empty;
        }


        public static Uzura ToUzuraFromJsonString(this string str)
        {
            var result = JsonConvert.DeserializeObject<Uzura>(str);
            if (result == null)
            {
                result = new Uzura(0, 0, 0, 0);
            }
            return result;
        }

        public static string ToJson(this string input)
        {
            return JsonConvert.SerializeObject(input);
        }

    }
}
