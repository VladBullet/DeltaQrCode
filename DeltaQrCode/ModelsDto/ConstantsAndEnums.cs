using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    using System.ComponentModel.DataAnnotations;

    public class ConstantsAndEnums
    {
    }
    public enum AppointmentType
    {
        Vulcanizare = 1,
        Mecanica = 2
    }

    public enum TyreType
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
}
