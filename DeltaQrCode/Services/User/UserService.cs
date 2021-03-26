using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services
{
    public class UserService : IUserService
    {
        private readonly AuthDbContext _authDb;
        public UserService(AuthDbContext dbContext)
        {
            _authDb = dbContext;
        }
        public CaUsers FindUserByLoginAndPass(string login, string password)
        {
            var result = _authDb.CaUsers.FirstOrDefault(x => (x.UserAccount == login || x.UserEmailAddress == login) && x.UserPassword == password);
            if (result == null || string.IsNullOrEmpty(result.UserAccount))
            {
                result = null;
            }
            return result;
        }
    }
}
