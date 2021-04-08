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

        public static readonly string[] Diametru = {"R13","R14", "R15", "R16", "R17", "R18", "R19", "R20", "R21", "R22"}; 

        public static readonly string[] Latime = { "125", "135", "145", "155", "165", "175", "185", "195", "205", "215", "225", "235", "245", "255", "265", "275", "285", "295", "305", "315", "325", "335", "345", "355"}; 

        public static readonly string[] Inaltime = { "35", "40", "45", "50", "55", "60", "65", "70" }; 

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
