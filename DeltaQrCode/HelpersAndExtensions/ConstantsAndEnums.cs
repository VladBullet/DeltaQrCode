using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode
{
    public static class ConstantsAndEnums
    {

        public static readonly Dictionary<int, string> RowsDictionary = new Dictionary<int, string> { { 1, "A" }, { 2, "B" }, { 3, "C" }, { 4, "D" }, { 5, "E" } };

        public static readonly Dictionary<int, string> TimeDictionary = new Dictionary<int, string> { { 30, "30 min" }, { 60, "1 ora" }, { 90, "1 ora 30 min" }, { 120, "2 ore" }, { 150, "2 ore 30 min" }, { 180, "3 ore" }, { 210, "3 ore 30 min" }, { 240, "4 ore" }, { 270, "4 ore 30 min" }, { 300, "5 ore" }, { 330, "5 ore 30 min" }, { 360, "6 ore" }, { 390, "6 ore 30 min" }, { 420, "7 ore" }, { 450, "7 ore 30 min" }, { 480, "8 ore" } };

        public static readonly string[] Diametru = {"12","13" }; 

        public static readonly string[] Latime = { "12", "13" }; 

        public static readonly string[] Inaltime = { "12", "13" }; 

        public static readonly string[] Intervals = { "A", "B", "C", "D", "E" };

        public static readonly int RandMax = 20; // TODO : --- VLAD --- : MAKE SURE WHAT THE MAX LIMITS ARE AND SET THEM RIGHT
        public static readonly int PozMax = 3; // TODO : --- VLAD --- : MAKE SURE WHAT THE MAX LIMITS ARE AND SET THEM RIGHT
        
    }
    public enum ServiceType
    {
        Vulcanizare = 1,
        Mecanica = 2
    }

    public enum TireType
    {
        Vara = 1,
        Iarna = 2,
        AllSeason = 3
    }

    public enum ActionType
    {
        Add = 1,
        Edit = 2,
        Info = 3
    }
    public enum OperatiuneSpalare
    {
        Interior = 1,
        Exterior = 2,
        InteriorExterior = 3
    }
    public enum StatusAnvelope
    {
        InRaft = 1,
        Casate = 2,
        Predate = 3,
        Montate = 4
    }
    
}
