using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using RAMHX.CMS.DataAccess;

namespace RAMHX.CMS.Web
{
    public class SiteContext
    {
        public static string AccessingSiteUsers
        {
            get
            {
                if (HttpContext.Current.Application["TotalOnlineUsers"] != null)
                {
                    return HttpContext.Current.Application["TotalOnlineUsers"].ToString();
                }
                return "0";
            }
        }
        public static System.Security.Principal.IIdentity CurrentUser
        {
            get
            {
                if (HttpContext.Current.User != null)
                    return HttpContext.Current.User.Identity;
                else
                    return null;
            }
        }
        public static string CurrentUser_SessionKey
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUser_SessionKey"] == null || string.IsNullOrEmpty(HttpContext.Current.Session["CurrentUser_SessionKey"].ToString())) 
                {
                    string guid = Guid.NewGuid().ToString();
                    if (CurrentUser == null)
                    {
                        guid = CurrentUser_Guid.ToString();
                    }
                    string key = Base64Encode(guid + "_" + DateTime.Now.Ticks.ToString());
                    HttpContext.Current.Session["CurrentUser_SessionKey"] = key;
                    return key;
                }
                else
                {
                    return HttpContext.Current.Session["CurrentUser_SessionKey"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["CurrentUser_SessionKey"] = value;
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static bool CurrentUser_IsAdmin
        {
            get
            {
                if (CurrentUser_DBObject != null)
                {
                    return CurrentUser_DBObject.AspNetRoles.FirstOrDefault(role => role.Name.ToLower() == "admin") != null;
                }

                return false;
            }
        }

        public static AspNetUser CurrentUser_DBObject
        {
            get
            {
                if (CurrentUser != null)
                {
                    DatabaseEntities dataContext = new DatabaseEntities();
                    return dataContext.AspNetUsers.FirstOrDefault(user => user.UserName == CurrentUser_UserName);
                }

                return null;
            }
        }

        public static List<AspNetRole> CurrentUser_Roles
        {
            get
            {
                if (CurrentUser != null)
                {
                    var curr = CurrentUser_DBObject;
                    if (curr != null)
                        return curr.AspNetRoles.ToList();
                }

                return new List<AspNetRole>();
            }
        }

        public static string CurrentUser_Name
        {
            get
            {

                return CurrentUser.Name;
            }
        }

        public static string CurrentUser_UserName
        {
            get
            {

                return CurrentUser.GetUserName();
            }
        }


        public static Guid CurrentUser_Guid
        {
            get
            {
                return Guid.Parse(CurrentUser.GetUserId());
            }
        }

        public static cms_Pages CurrentPage
        {
            get
            {
                if (HttpContext.Current.Items["CurrentPage"] != null && HttpContext.Current.Items["CurrentPage"] is cms_Pages)
                {
                    return (cms_Pages)HttpContext.Current.Items["CurrentPage"];
                }
                return null;
            }
        }

        public static RAMHX.CMS.Infra.Enums.PageMode CurrentPage_Mode
        {
            get
            {
                string mode = HttpContext.Current.Request.QueryString["rh_mode"] ?? string.Empty;
                mode = mode.ToUpper();
                if (mode == "EDIT")
                {
                    return Infra.Enums.PageMode.EDIT;
                }
                else if (mode == "PREVIEW")
                {
                    return Infra.Enums.PageMode.PREVIEW;
                }

                return Infra.Enums.PageMode.NORMAL;
            }
        }

        public static bool IsDialogPage
        {
            get
            {
                return (HttpContext.Current.Request.UrlReferrer.PathAndQuery.ToLower().Contains("dialog=1"));
            }
        }

        public static List<cms_Pages> Pages { get; set; }

        public static cms_Pages GetPageById(string pageid)
        {
            Guid pid;
            if (Guid.TryParse(pageid, out pid))
            {
                return GetPageById(pid);
            }
            return null;
        }

        public static cms_Pages GetPageById(Guid pageid)
        {
            return Pages.FirstOrDefault(page => page.PageID == pageid);
        }

        public static cms_Pages GetPageByPath(string pageItemPath)
        {
            if (!string.IsNullOrEmpty(pageItemPath))
                return Pages.FirstOrDefault(page => page.FullItemPath.ToLower() == pageItemPath.ToLower().Trim());

            return null;
        }

        public static bool HasCurrentUserInRole(string rolename)
        {
            try
            {
                rolename = rolename.ToLower();
                if (CurrentUser_DBObject != null)
                {
                    return CurrentUser_DBObject.AspNetRoles.FirstOrDefault(role => role.Name.ToLower() == rolename) != null;
                }
                return false;
            }
            catch { }
            return false;

        }
    }
}
