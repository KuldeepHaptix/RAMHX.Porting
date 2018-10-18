using Microsoft.AspNetCore.Http;
using RAMHX.CMS.DataAccessCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAMHX.CMS.InfraCore
{
    public class SiteContext:BaseClass
    {
        public SiteContext(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        
        public  string AccessingSiteUsers
        {
            
            get
            {
            
                //if (HttpContext.Current.Application["TotalOnlineUsers"] != null)
                //{
                //    return HttpContext.Current.Application["TotalOnlineUsers"].ToString();
                //}
                return "0";
            }
        }
        public  System.Security.Principal.IIdentity CurrentUser
        {
            get
            {
                if (this.Context.HttpContext.User != null)
                    return this.Context.HttpContext.User.Identity;
                else
                    return null;
            }
        }
        public  string CurrentUser_SessionKey
        {
            get
            {
                if (this.Context.HttpContext.Session.GetString("CurrentUser_SessionKey") == null || string.IsNullOrEmpty(this.Context.HttpContext.Session.GetString("CurrentUser_SessionKey")))
                {
                    string guid = Guid.NewGuid().ToString();
                    if (CurrentUser == null)
                    {
                        guid = CurrentUser_Guid.ToString();
                    }
                    string key = Base64Encode(guid + "_" + DateTime.Now.Ticks.ToString());
                    this.Context.HttpContext.Session.SetString("CurrentUser_SessionKey", key);
                    return key;
                }
                else
                {
                    return this.Context.HttpContext.Session.GetString("CurrentUser_SessionKey");
                }
            }
            set
            {
                this.Context.HttpContext.Session.SetString("CurrentUser_SessionKey", value);
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
        public  bool CurrentUser_IsAdmin
        {
            get
            {
                if (CurrentUser_DBObject != null)
                {
                    return CurrentUser_DBObject.AspNetUserRoles.FirstOrDefault(role => role.Role.Name.ToLower() == "admin") != null;
                }

                return false;
            }
        }

        public  AspNetUsers CurrentUser_DBObject
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

        public  List<AspNetRoles> CurrentUser_Roles
        {
            get
            {
                if (CurrentUser != null)
                {
                    var curr = CurrentUser_DBObject;
                    if (curr != null)
                        return curr.AspNetRoles.ToList();
                }

                return new List<AspNetRoles>();
            }
        }

        public  string CurrentUser_Name
        {
            get
            {

                return CurrentUser.Name;
            }
        }

        public  string CurrentUser_UserName
        {
            get
            {

                return _userManager.GetUserName(this.Context.HttpContext.User);
            }
        }


        public  Guid CurrentUser_Guid
        {
            get
            {
                return Guid.Parse(_userManager.GetUserId(this.Context.HttpContext.User));
            }
        }

        public  CmsPages CurrentPage
        {
            get
            {
                if (this.Context.HttpContext.Items["CurrentPage"] != null && this.Context.HttpContext.Items["CurrentPage"] is CmsPages)
                {
                    return (CmsPages)this.Context.HttpContext.Items["CurrentPage"];
                }
                return null;
            }
        }

        public  RAMHX.CMS.InfraCore.Enums.PageMode CurrentPage_Mode
        {
            get
            {
                var qsKey = this.Context.HttpContext.Request.Query.FirstOrDefault(qs => qs.Key == "rh_mode");
                string mode = qsKey.Key.ToString() ?? string.Empty;
                mode = mode.ToUpper();
                if (mode == "EDIT")
                {
                    return InfraCore.Enums.PageMode.EDIT;
                }
                else if (mode == "PREVIEW")
                {
                    return InfraCore.Enums.PageMode.PREVIEW;
                }

                return InfraCore.Enums.PageMode.NORMAL;
            }
        }

        public  bool IsDialogPage
        {
            get
            {
                return (this.Context.HttpContext.Request.Path.Value.ToLower().Contains("dialog=1"));
            }
        }

        public static List<CmsPages> Pages { get; set; }

        public static CmsPages GetPageById(string pageid)
        {
            Guid pid;
            if (Guid.TryParse(pageid, out pid))
            {
                return GetPageById(pid);
            }
            return null;
        }

        public static CmsPages GetPageById(Guid pageid)
        {
            return Pages.FirstOrDefault(page => page.PageId == pageid);
        }

        public static CmsPages GetPageByPath(string pageItemPath)
        {
            if (!string.IsNullOrEmpty(pageItemPath))
                return Pages.FirstOrDefault(page => page.FullItemPath.ToLower() == pageItemPath.ToLower().Trim());

            return null;
        }

        public  bool HasCurrentUserInRole(string rolename)
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
