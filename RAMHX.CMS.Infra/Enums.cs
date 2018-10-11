using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMHX.CMS.Infra
{
    public class Enums
    {
        public class AppConfigs
        {
            public enum AccesDeniedGroupitem
            {
                ItemId = 1,
                GroupId = 1
            }

            public enum PageNotFoundItem
            {
                ItemId = 3,
                GroupId = 1
            }

            public enum LoginPageItem
            {
                ItemId = 2,
                GroupId = 1
            }

            public enum Groups
            {
                AppSettings = 1,
                FieldTypes = 2
            }

            public enum PackageInstallPath
            {
                AppSettings = 1,
                PackageInstallPath = 4
            }
            public enum PackageAPIKey
            {
                AppSettings = 1,
                PackageAPIKey = 5
            }

        }

        public enum PageMode
        {
            NORMAL = 1,
            EDIT = 2,
            PREVIEW = 3
        }

        public enum FieldTypes
        {
            SingleLine = 1,
            Date = 3,
            Time = 6
        }
    }
}
