using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using RAMHX.CMS.DataAccessCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RAMHX.CMS.InfraCore
{
    public class Common:BaseClass
    {
        public Common(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        public static string GetLoginPassword(string firstName, string mobile)
        {
            firstName = new String(firstName.Where(Char.IsLetter).ToArray()).ToLower();
            mobile = new String(mobile.Where(Char.IsNumber).ToArray()).ToLower();

            if (firstName.Length > 4)
            {
                firstName = firstName.Substring(0, 4);
            }
            if (mobile.Length > 4)
            {
                mobile = mobile.Substring(mobile.Length - 4, 4);
            }
            return firstName + "@" + mobile;
        }
        /// <summary>
        /// It gets boolean value from query string
        /// </summary>
        /// <param name="key">key of query string</param>
        /// <returns>returns id</returns>
        public  bool QSBoolValue(string key)
        {
            bool id = false;
            var qsKey = this.Context.HttpContext.Request.Query.FirstOrDefault(qs => qs.Key == key);
            if (!string.IsNullOrEmpty(qsKey.Key.ToString()) &&
                (qsKey.Key.ToString() == "1" || qsKey.Key.ToString().ToLower() == "true"))
            {
                id = true;
            }

            return id;
        }

        /// <summary>
        /// It will parse the value into integer from string value
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>integer value</returns>
        public static int GetIntValue(string value)
        {
            int val = 0;
            int.TryParse(value, out val);
            return val;
        }

        public static Guid GetGuidValue(string value)
        {
            Guid val = Guid.Empty;
            Guid.TryParse(value, out val);
            return val;
        }


        public static bool HasCorrectConnectionString
        {
            get
            {
                return ValidateConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
        }

        public static bool ValidateConnectionString(string connestionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connestionString))
                {
                    conn.Open(); // throws if invalid
                }
                return true;
                //                    return !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
            catch
            {
                return false;
            }

        }

        public static bool ValidateUrl(string url)
        {
            List<string> lstAvoidRequests = new List<string>() {
                    "/admin",
                    "/bundles",
                    "/__browserlink"
                };

            var filePath = Path.Combine(url).FirstOrDefault().ToString();
            return (lstAvoidRequests.Count(re => url.StartsWith(re)) == 0 && !System.IO.File.Exists(filePath));
        }

        public const string DEFAULT_PACKAGE_PATH = "/areas/admin/data/packages/";


        public const string PackagePath = "PackagePath";

        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }


        public class Constants
        {
            public class AppConfigItemCodes
            {
                public const string RAMHXPackageInstallPath = "RAMHX.Package.InstallPath";
                public const string RAMHXPackageAPIKey = "RAMHX.Package.APIKey";
            }

        }

    }
}
