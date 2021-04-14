using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.HelpersAndExtensions
{
    public interface IHttpHelper
    {
        string GetGuestClientLink(string code);
    }
}
