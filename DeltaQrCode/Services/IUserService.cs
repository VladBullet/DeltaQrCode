using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    public interface IUserService
    {
        CaUsers FindUserByLoginAndPass(string login, string password);
    }
}
