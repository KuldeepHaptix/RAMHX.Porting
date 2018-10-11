
using RAMHX.CMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMHX.CMS.Repository
{
    public class UserModel
    {
        public AspNetUser Users;
        public string AssignedRoles { get; set; }
    }
}
