using DeltaQrCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.HelpersAndExtensions
{
    using System.Security.Cryptography;
    using System.Text;

    using DeltaQrCode.ModelsDto;

    public static class Helpers
    {
        #region "DateTime Helpers"

        public static DateTime GetStartDateFromStringParam(string dateString)
        {
            if (String.IsNullOrWhiteSpace(dateString))
            {
                dateString = DateTime.Now.Date.ToString();
            }

            DateTime dt;
            if (DateTime.TryParse(dateString, out dt))
            {
                return dt;
            }

            return DateTime.Now;

        }

        /// <summary>
        /// Return a list of dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<DateTime> CreateAvailableDatesList(DateTime startDate)
        {
            var numberOfDaysToShow = 31;
            List<DateTime> availableDatesList = new List<DateTime>();
            for (int i = 0; i < numberOfDaysToShow; i++)
            {
                availableDatesList.Add(startDate.Date.AddDays(i));
            }

            return availableDatesList;
        }

        #endregion

        public static List<Position> GetAllCombinationsRowsAndPositionsAndIntervals()
        {
            var intervale = ConstantsAndEnums.Intervals;
            var listaDePozitii = new List<string>();
            for (int i = 1; i <= ConstantsAndEnums.PozMax; i++)
            {
                listaDePozitii.Add(i.ToString());
            }
            var randuri = new List<string>();
            for (int i = 1; i <= ConstantsAndEnums.RandMax; i++)
            {
                randuri.Add(i.ToString());
            }

            var pozitii = listaDePozitii.ToArray();
            var randuriArray = randuri.ToArray();

            //var generated = randuri.Where(x => x != null)
            //    .SelectMany(g => pozitii.Where(c => c != null)
            //        .Select(c => new Position(g, c))
            //    ).ToList();

            var gen = randuriArray.Where(x => x != null)
                .SelectMany(
                    y => pozitii.Where(c => c != null)
                        .SelectMany(
                            h => intervale.Where(a => a != null)
                                .Select(f => new Position(y, h, f)))).ToList();
            return gen;
        }
    }

    public static class EncriptionHelpers
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
