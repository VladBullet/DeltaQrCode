namespace DeltaQrCode.HelpersAndExtensions
{
    using System;
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
            string result = string.Format("Diam:{0}, Lat:{1}, H:{2}", dim.Diam, dim.Lat, dim.H);
            if (dim.Dot != null)
            {
                result = string.Format(result + ", Dot:{0}", dim.Dot);
            }
            return result;
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
            string result = string.Format("StF:{0}", uz.StF);
            if (uz.DrF != null)
            {
                result = String.Format(result + ", DrF:{0}", uz.DrF);
            }
            if (uz.StS != null)
            {
                result = String.Format(result + ", StS:{0}", uz.StS);
            }
            if (uz.DrS != null)
            {
                result = String.Format(result + ", DrS:{0}", uz.DrS);
            }
            return result;
        }

        public static string ToDisplayString(this HotelPositionsDto pos)
        {
            if (pos == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(pos.Pozitie) && string.IsNullOrEmpty(pos.Rand) && string.IsNullOrEmpty(pos.Interval))
            {
                return string.Empty;
            }

            return string.Format("Rand:{0}, Poz:{1}, Int:{2}, Ocupate:{3}", pos.Rand, pos.Pozitie, pos.Interval, pos.Locuriocupate);
        }

        public static string ToSetAnvPositionString(this AnvelopaDto setanv)
        {
            if (setanv.Pozitie == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(setanv.Pozitie.Pozitie) && string.IsNullOrEmpty(setanv.Pozitie.Rand) && string.IsNullOrEmpty(setanv.Pozitie.Interval))
            {
                return string.Empty;
            }

            return string.Format("Rand:{0}, Poz:{1}, Int:{2}", setanv.Pozitie.Rand, setanv.Pozitie.Pozitie, setanv.Pozitie.Interval);
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
