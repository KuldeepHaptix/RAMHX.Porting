using System;
using System.Collections.Generic;
using System.Text;
using RAMHX.CMS.DataAccessCore;

namespace RAMHX.CMS.RepositoryCore
{
    public class UserModel
    {
        public AspNetUsers Users;
        public string AssignedRoles { get; set; }
    }
}
