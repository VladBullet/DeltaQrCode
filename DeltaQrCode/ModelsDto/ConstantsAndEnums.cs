using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Policy;

    public static class ConstantsAndEnums
    {

        public static readonly Dictionary<int, string> RowsDictionary = new Dictionary<int, string> { { 1, "A" }, { 2, "B" }, { 3, "C" }, { 4, "D" }, { 5, "E" } };

        public static readonly string[] Intervals = { "A", "B", "C", "D", "E" };
        
        public static readonly int RandMax = 20; // TODO : --- VLAD --- : MAKE SURE WHAT THE MAX LIMITS ARE AND SET THEM RIGHT
        public static readonly int PozMax = 3; // TODO : --- VLAD --- : MAKE SURE WHAT THE MAX LIMITS ARE AND SET THEM RIGHT

    }
    public enum AppointmentType
    {
        Vulcanizare = 1,
        Mecanica = 2
    }

    public enum TireType
    {
        Summer = 1,
        Winter = 2,
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
}
