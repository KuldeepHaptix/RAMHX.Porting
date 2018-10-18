using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RAMHX.CMS.WebCore.Areas.Identity.Pages.Account;
using RAMHX.CMS.DataAccessCore;
using RAMHX.CMS.RepositoryCore;

namespace RAMHX.CMS.WebCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        DatabaseEntities db = new DatabaseEntities();

        private readonly RoleManager<IdentityRole> roleManager;
       
        public AccountController(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();

        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserList()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public JsonResult GetUserList(string keywords, string roleId)
        {
            List<string> userIds = new List<string>();
            var roleName = string.Empty;
            var applyRoleFilter = false;
            if (!string.IsNullOrEmpty(roleId))
            {
                AspNetRoles aspNetRole = db.AspNetRoles.Find(roleId);
                if (aspNetRole != null)
                {
                    applyRoleFilter = true;
                    userIds = aspNetRole.AspNetUserRoles.Select(u => u.UserId).ToList();
                    roleName = aspNetRole.Name;
                }
            }
            var usersdata1 = db.AspNetUsers.Where(u => u.UserName.Contains(keywords) || u.Email.Contains(keywords) || u.FirstName.Contains(keywords) || u.LastName.Contains(keywords) || u.Mobile.Contains(keywords) || u.Address.Contains(keywords) || u.City.Contains(keywords));
            List<AspNetUsers> usersdata = new List<AspNetUsers>();
            if (applyRoleFilter)
            {
                usersdata = usersdata1.Where(usr => userIds.Contains(usr.Id)).Take(100).ToList();
            }
            else
            {
                usersdata = usersdata1.Take(100).ToList();
            }

            

            List<UserModel> model = new List<UserModel>();

            foreach (var item in usersdata)
            {
                UserModel mdl = new UserModel();
                mdl.Users = new AspNetUsers()
                {
                    Address = item.Address,
                    City = item.City,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Id = item.Id,
                    Gender = item.Gender,
                    UserName = item.UserName,
                    Mobile = item.Mobile
                };
                //mdl.Users = item;


                var uroles = this.roleManager.Roles.ToList();
                if ( string.IsNullOrEmpty(uroles.ToString()))
                {
                string selectedRole = string.Empty;
                   foreach (var role in uroles)
                   {
                        selectedRole = selectedRole + role + ",";
                    }
                    mdl.AssignedRoles = selectedRole.TrimEnd(',');
                    model.Add(mdl);
                }
            }
            model = model.Take(100).ToList();
            return Json(new { Users = model });

        }
       

        public  IActionResult LogOff(string returnUrl = null)
        {
             _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return LocalRedirect("/");
                
            }
        }
    }
}