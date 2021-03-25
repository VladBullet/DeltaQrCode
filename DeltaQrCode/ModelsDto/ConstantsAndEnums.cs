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

    public enum CreateOrEdit
    {
        Create = 1,
        Edit = 2
    }
}
