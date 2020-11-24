using System;
using System.Collections.Generic;

namespace DeltaQrCode
{
    public class CaUsers
    {
        public CaUsers() { }
        public CaUsers(CaUsers user, bool withPassword = false)
        {
            Id = user.Id;
            UserAccount = user.UserAccount;
            UserFirstName = user.UserFirstName;
            UserLastName = user.UserLastName;
            UserEmailAddress = user.UserEmailAddress;
            UserMobile = user.UserMobile;
            UserPhone = user.UserPhone;
            UserPassword = withPassword ? user.UserPassword : string.Empty;
            UserCompany = user.UserCompany;
            UserRights = user.UserRights;
            UserRightsAdmin = user.UserRightsAdmin;
            UserRightsBycompany = user.UserRightsBycompany;
            UserLock = user.UserLock;
        }
        public int Id { get; set; }
        public string UserAccount { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserMobile { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public string UserCompany { get; set; }
        public string UserRights { get; set; }
        public string UserRightsAdmin { get; set; }
        public string UserRightsBycompany { get; set; }
        public int UserLock { get; set; }

    }
}
