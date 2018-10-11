using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using RAMHX.CMS.Web.Areas.Admin.Models;
using RAMHX.CMS.DataAccess;
using System.Web.Security;
using System.Text;
using RAMHX.CMS.Repository;
using System.Configuration;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();
        private AppConfiguration appConfig = new AppConfiguration();
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string redirect)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!string.IsNullOrEmpty(redirect))
            {
                Response.Redirect(redirect, true);
                return null;
            }
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
                AspNetRole aspNetRole = db.AspNetRoles.Find(roleId);
                if (aspNetRole != null)
                {
                    applyRoleFilter = true;
                    userIds = aspNetRole.AspNetUsers.Select(u => u.Id).ToList(); 
                    roleName = aspNetRole.Name;
                }
            }
            var usersdata1 = db.AspNetUsers.Where(u => u.UserName.Contains(keywords) || u.Email.Contains(keywords) || u.FirstName.Contains(keywords) || u.LastName.Contains(keywords) || u.Mobile.Contains(keywords) || u.Address.Contains(keywords) || u.City.Contains(keywords));
            List<AspNetUser> usersdata = new List<AspNetUser>();
            if (applyRoleFilter)
            {
                usersdata = usersdata1.Where(usr => userIds.Contains(usr.Id)).Take(100).ToList();
            }
            else
            {
                usersdata = usersdata1.Take(100).ToList();
            }

            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            List<UserModel> model = new List<UserModel>();

            foreach (var item in usersdata)
            {
                UserModel mdl = new UserModel();
                mdl.Users = new AspNetUser()
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
                var uroles = UserManager.GetRoles(item.Id);
                if (uroles.Contains(roleName) || string.IsNullOrEmpty(roleName))
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
            return Json(new { Users = model }, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {
            var usersdata = db.AspNetUsers.ToList();

            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            List<UserModel> model = new List<UserModel>();

            foreach (var item in usersdata)
            {
                UserModel mdl = new UserModel();
                mdl.Users = item;
                var uroles = UserManager.GetRoles(item.Id);
                string selectedRole = string.Empty;
                foreach (var role in uroles)
                {
                    selectedRole = selectedRole + role + ",";
                }
                mdl.AssignedRoles = selectedRole.TrimEnd(',');
                model.Add(mdl);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UsersByRole(string rid)
        {
            var usersdata = db.GetAspNetUsersByRole(rid);
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            List<UserModel> model = new List<UserModel>();
            foreach (var item in usersdata)
            {
                UserModel mdl = new UserModel();
                mdl.Users = new AspNetUser() { Address = item.Address, City = item.City, Email = item.Email, FirstName = item.FirstName, Gender = item.Gender, Id = item.Id, LastName = item.LastName, Mobile = item.Mobile, UserName = item.UserName };
                var uroles = UserManager.GetRoles(item.Id);
                string selectedRole = string.Empty;
                foreach (var role in uroles)
                {
                    selectedRole = selectedRole + role + ",";
                }
                mdl.AssignedRoles = selectedRole.TrimEnd(',');
                model.Add(mdl);
            }

            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    SQLDataAccess.TakeAutoDailyBackup(false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        [AllowAnonymous]
        public async Task<JsonResult> PublicLogin(FormCollection form)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            if (ModelState.IsValid)
            {
                string frmEmail = form["email"];
                var aspNetUsers = db.AspNetUsers.Where(usr => usr.UserName == frmEmail).ToList();
                if ((aspNetUsers == null || aspNetUsers.Count() == 0) && AppConfiguration.GetAppSettings("RAMHX.AllowLoginWithMobile") == "1")
                {
                    aspNetUsers = db.AspNetUsers.Where(usr => usr.Mobile == frmEmail).ToList();
                }
                if ((aspNetUsers == null || aspNetUsers.Count() == 0) && AppConfiguration.GetAppSettings("RAMHX.AllowLoginWithEmail") == "1")
                {
                    aspNetUsers = db.AspNetUsers.Where(usr => usr.Email == frmEmail).ToList();
                }
                if ((aspNetUsers == null || aspNetUsers.Count() == 0))
                {
                    return Json(new { status = "fail", message = "Invalid username or password" }, JsonRequestBehavior.AllowGet);
                }
                if (aspNetUsers.Count() > 1)
                {
                   
                    logger.Info("There are multiple users with mobile/email " + frmEmail + ". Please verify and therefore not allowing to login");
                    return Json(new { status = "fail", message = "Invalid username or password" }, JsonRequestBehavior.AllowGet);
                }
                string uname = aspNetUsers.First().UserName;
                var user = await UserManager.FindAsync(uname, form["password"]);
                if (user != null)
                {
                    await SignInAsync(user, false);
                    SQLDataAccess.TakeAutoDailyBackup(false);
                    return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = "fail", message = "Invalid username or password" }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        //
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            RegisterViewModel register = new RegisterViewModel() { SendPasswordInEmail = true };
            return View(register);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateUser(string id)
        {
            return View(db.AspNetUsers.First(user => user.Id == id));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateUser(AspNetUser user)
        {
            var editUser = db.AspNetUsers.First(usr => usr.Id == user.Id);
            editUser.FirstName = user.FirstName;
            editUser.LastName = user.LastName;
            editUser.Address = user.Address;
            editUser.City = user.City;
            editUser.Mobile = user.Mobile;
            editUser.Gender = user.Gender;
            db.SaveChanges();
            if (SiteContext.IsDialogPage)
            {
                Response.Redirect("/Admin/Account/UsersByRole" + Request.UrlReferrer.Query);
                //return RedirectToAction("Users", new { @dialog = 1 });
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateUserDetails(AspNetUser user)
        {
            var editUser = db.AspNetUsers.First(usr => usr.Id == user.Id);
            editUser.FirstName = user.FirstName;
            editUser.LastName = user.LastName;
            editUser.Address = user.Address;
            editUser.City = user.City;
            editUser.Mobile = user.Mobile;
            editUser.Gender = user.Gender;
            editUser.Email = user.Email;
            db.SaveChanges();
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Account/Register
        [HttpPost]
        public JsonResult PublicRegister(FormCollection form)
        {
            RegisterViewModel model = new RegisterViewModel()
            {
                Address = form["address"],
                City = form["city"],
                ConfirmPassword = form["confirmpassword"],
                Email = form["email"],
                Username = form["username"],
                FirstName = form["firstname"],
                LastName = form["lastname"],
                Mobile = form["mobile"],
                Password = form["password"],
                SendPasswordInEmail = Convert.ToBoolean(form["sendpasswordinemail"])
            };
            if (ModelState.IsValid)
            {
                if (AppConfiguration.GetAppSettings("RAMHX.AllowLoginWithMobile") == "1")
                {
                    var mobUser = db.AspNetUsers.FirstOrDefault(u => u.Mobile == model.Mobile);
                    if (string.IsNullOrEmpty(model.Mobile))
                    {
                        List<string> errors = new List<string>();
                        errors.Add("Please enter Mobile number");
                        return Json(new { status = "fail", message = errors }, JsonRequestBehavior.AllowGet);
                    }
                    else if (mobUser != null)
                    {
                        List<string> errors = new List<string>();
                        errors.Add("Entered Mobile '" + model.Mobile + "' is already registered");
                        return Json(new { status = "fail", message = errors }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (AppConfiguration.GetAppSettings("RAMHX.AllowLoginWithEmail") == "1")
                {
                    var mobUser = db.AspNetUsers.FirstOrDefault(u => u.Email == model.Email);
                    if (string.IsNullOrEmpty(model.Email))
                    {
                        List<string> errors = new List<string>();
                        errors.Add("Please enter your email");
                        return Json(new { status = "fail", message = errors }, JsonRequestBehavior.AllowGet);
                    }
                    else if (mobUser != null)
                    {
                        List<string> errors = new List<string>();
                        errors.Add("Entered Email '" + model.Email + "' is already registered");
                        return Json(new { status = "fail", message = errors }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (AppConfiguration.GetAppSettings("Registration.RequiredEmail") != "1" && string.IsNullOrEmpty(model.Username))
                {
                    model.Username = model.Email;
                }

                var user = new ApplicationUser() { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Address = model.Address, City = model.City, Mobile = model.Mobile };
                IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    UpdateUserProfile(user);
                    if (model.SendPasswordInEmail)
                    {
                        SendEmailRepo em = new SendEmailRepo();
                        string message = string.Format("<h3>Your Login Details</h3><div><b>Username:</b>{0}</div><div><b>Password:</b>{1}</div>", model.Email, model.Password);
                        em.SendEmail("Your login details", model.Email, AppConfiguration.GetAppSettings("From.EmailID"), message);
                    }

                    return Json(new { status = "success", newuser = user }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = "fail", message = result }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { status = "fail", message = "please correct errors" }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Address = model.Address, City = model.City, Mobile = model.Mobile };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string rid = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["rid"];
                    if (string.IsNullOrEmpty(rid))
                    {
                        rid = model.RoleId;
                    }
                    if (!string.IsNullOrEmpty(rid))
                    {
                        PageRepository pr = new PageRepository();
                        var role = pr.GetRoleByRoleID(rid);
                        UserManager.AddToRole(user.Id, role.Name);
                    }

                    UpdateUserProfile(user);
                    //await SignInAsync(user, isPersistent: false);
                    if (model.SendPasswordInEmail)
                    {
                        SendEmailRepo em = new SendEmailRepo();
                        string message = string.Format("<h3>Your Login Details</h3><div><b>Username:</b>{0}</div><div><b>Password:</b>{1}</div>", model.Email, model.Password);
                        em.SendEmail("Your login details", model.Email, AppConfiguration.GetAppSettings("From.EmailID"), message);
                    }
                    if (SiteContext.IsDialogPage)
                    {
                        Response.Redirect("UsersByRole" + Request.UrlReferrer.Query);
                    }
                    return RedirectToAction("Users");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> CreateUser(RegisterViewModel model)
        {
            {
                var user = new ApplicationUser() { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Address = model.Address, City = model.City, Mobile = model.Mobile };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string rid = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["rid"];
                    if (string.IsNullOrEmpty(rid))
                    {
                        rid = model.RoleId;
                    }
                    if (!string.IsNullOrEmpty(rid))
                    {
                        PageRepository pr = new PageRepository();
                        var role = pr.GetRoleByRoleID(rid);
                        UserManager.AddToRole(user.Id, role.Name);
                    }

                    UpdateUserProfile(user);
                    //await SignInAsync(user, isPersistent: false);
                    if (model.SendPasswordInEmail)
                    {
                        SendEmailRepo em = new SendEmailRepo();
                        string message = string.Format("<h3>Your Login Details</h3><div><b>Username:</b>{0}</div><div><b>Password:</b>{1}</div>", model.Email, model.Password);
                        em.SendEmail("Your login details", model.Email, AppConfiguration.GetAppSettings("From.EmailID"), message);
                    }
                }
                else
                {
                    return Json(new { status = "fail", errors = result }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);

        }

        private void UpdateUserProfile(ApplicationUser user)
        {
            var userData = db.AspNetUsers.FirstOrDefault(u => u.UserName == user.UserName);
            if (userData != null)
            {
                userData.FirstName = user.FirstName;
                userData.LastName = user.LastName;
                userData.Address = user.Address;
                userData.City = user.City;
                userData.Mobile = user.Mobile;
                db.SaveChanges();
            }

        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        // POST: /Admin/Account/PublicForgotPassword
        [HttpPost]
        [AllowAnonymous]
        public JsonResult PublicForgotPassword(FormCollection form)
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel() { Email = form["email"].ToString() };
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(model.Email);
                if (user == null)
                {
                    return Json(new { status = "fail", message = "Invalid email or account does not exists" }, JsonRequestBehavior.AllowGet);
                }

                //For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                var configItem = appConfig.GetGroupItemByItemAndGroupID("ResetPasswordUrl", 1);

                string code = UserManager.GeneratePasswordResetToken(user.Id);
                string callbackUrl = "";
                if (configItem == null || string.IsNullOrEmpty(configItem.ItemDesc))
                {
                    callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                }
                else
                {
                    callbackUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + Request.Url.Host + (Request.Url.Port == 80 ? "" : ":" + Request.Url.Port.ToString()) + configItem.ItemDesc + "?userid=" + user.Id + "&code=" + code;
                }

                log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Info("Reset Password Link for " + model.Email + " is " + callbackUrl);

                UserManager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                    return View();
                }

                //For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                var configItem = appConfig.GetGroupItemByItemAndGroupID("ResetPasswordUrl", 1);

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                string callbackUrl = "";
                if (configItem == null || string.IsNullOrEmpty(configItem.ItemDesc))
                {
                    callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                }
                else
                {
                    callbackUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + Request.Url.Host + (Request.Url.Port == 80 ? "" : ":" + Request.Url.Port.ToString()) + configItem.ItemDesc + "?userid=" + user.Id + "&code=" + code;
                }

                log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Info("Reset Password Link for " + model.Email + " is " + callbackUrl);

                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

                var configFPItem = appConfig.GetGroupItemByItemAndGroupID("ForgotPasswordConfirmationUrl", 1);
                if (configFPItem == null || string.IsNullOrEmpty(configFPItem.ItemDesc))
                {
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
                else
                {
                    Response.Redirect(configFPItem.ItemDesc);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        public JsonResult PublicResetPassword(FormCollection form)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel() { Code = form["code"], Email = form["email"], Password = form["password"], ConfirmPassword = form["confirmpassword"] };
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(model.Email);
                if (user == null)
                {
                    return Json(new { status = "fail", message = "Invalid email or account does not exists" }, JsonRequestBehavior.AllowGet);
                }
                else if (model.Password != model.ConfirmPassword)
                {
                    return Json(new { status = "fail", message = "Password and Confirm password are not same" }, JsonRequestBehavior.AllowGet);
                }
                IdentityResult result = UserManager.ResetPassword(user.Id, model.Code, model.Password);
                if (!result.Succeeded)
                {
                    return Json(new { status = "fail", message = "Password could not reset. Please try later or contact administrator", result = result }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    var configItem = appConfig.GetGroupItemByItemAndGroupID("ResetPasswordConfirmationUrl", 1);
                    if (configItem == null || string.IsNullOrEmpty(configItem.ItemDesc))
                    {
                        if (SiteContext.IsDialogPage)
                        {
                            return RedirectToAction("ResetPasswordConfirmation", "Account", new { @dialog = 1 });
                        }

                        return RedirectToAction("ResetPasswordConfirmation", "Account");
                    }
                    else
                    {
                        Response.Redirect(configItem.ItemDesc);
                    }
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            if (SiteContext.IsDialogPage)
            {
                return RedirectToAction("Manage", new { Message = message, @dialog = 1 });
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        [Authorize]
        public string DeleteAccount(string userid)
        {
            var uid = Guid.Parse(userid);
            if (SiteContext.CurrentUser_Guid != uid)
            {
                db.AspNetUsers.Remove(db.AspNetUsers.First(u => u.Id == userid));
                db.SaveChanges();
                return "1";
            }

            return "2";
        }

        // GET: /Account/Manage
        public ActionResult Manage(string id, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.Userid = id;
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        [HttpPost]
        public JsonResult PublicChangePassword(FormCollection form)
        {
            ManageUserViewModel model = new ManageUserViewModel()
            {
                id = form["id"],
                ConfirmPassword = form["confirmpassword"],
                NewPassword = form["newpassword"],
                OldPassword = form["oldpassword"]
            };
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = new IdentityResult();
                    if (model.OldPassword != null)
                    {
                        result = UserManager.ChangePassword(model.id, model.OldPassword, model.NewPassword);
                    }
                    else
                    {
                        UserManager.RemovePassword(model.id);
                        result = UserManager.AddPassword(model.id, model.NewPassword);
                        //IdentityResult result = await UserManager.ChangePasswordAsync(model.id, model.OldPassword, model.NewPassword);
                    }
                    if (!result.Succeeded)
                    {
                        return Json(new { status = "fail", message = result }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = UserManager.AddPassword(User.Identity.GetUserId(), model.NewPassword);
                    if (!result.Succeeded)
                    {
                        return Json(new { status = "fail", message = result }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = new IdentityResult();
                    if (model.OldPassword != null)
                    {
                        result = await UserManager.ChangePasswordAsync(model.id, model.OldPassword, model.NewPassword);
                    }
                    else
                    {
                        UserManager.RemovePassword(model.id);
                        result = UserManager.AddPassword(model.id, model.NewPassword);
                        //IdentityResult result = await UserManager.ChangePasswordAsync(model.id, model.OldPassword, model.NewPassword);
                    }
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        await SignInAsync(user, isPersistent: false);
                        if (SiteContext.IsDialogPage)
                        {
                            Response.Redirect("Manage" + Request.UrlReferrer.Query + "&Message=" + ManageMessageId.ChangePasswordSuccess);
                        }
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        if (SiteContext.IsDialogPage)
                        {
                            return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess, @dialog = 1 });
                        }
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> AppAdminResetPassword(string userid, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return Json(new { status = "fail", message = "Please pass correct userid" }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                return Json(new { status = "fail", message = "Please enter password and confirm password" }, JsonRequestBehavior.AllowGet);
            }
            if (password != confirmPassword)
            {
                return Json(new { status = "fail", message = "Password does not match with confirm password" }, JsonRequestBehavior.AllowGet);
            }

            UserManager.RemovePassword(userid);
            IdentityResult result = await UserManager.AddPasswordAsync(userid, password);
            if (result.Succeeded)
            {
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "fail", result = result }, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> ForgotPWDResetPassword(string userid, string password, string confirmPassword)
        {
            return await AppAdminResetPassword(userid, password, confirmPassword);
            //if (string.IsNullOrEmpty(userid))
            //{
            //    return Json(new { status = "fail", message = "Please pass correct userid" }, JsonRequestBehavior.AllowGet);
            //}

            //if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            //{
            //    return Json(new { status = "fail", message = "Please enter password and confirm password" }, JsonRequestBehavior.AllowGet);
            //}
            //if (password != confirmPassword)
            //{
            //    return Json(new { status = "fail", message = "Password does not match with confirm password" }, JsonRequestBehavior.AllowGet);
            //}

            //UserManager.RemovePassword(userid);
            //IdentityResult result = await UserManager.AddPasswordAsync(userid, password);
            //if (result.Succeeded)
            //{
            //    return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { status = "fail", result = result }, JsonRequestBehavior.AllowGet);
            //}
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                if (SiteContext.IsDialogPage)
                {
                    return RedirectToAction("Login", new { @dialog = 1 });
                }
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                if (SiteContext.IsDialogPage)
                {
                    return RedirectToAction("Manage", new { Message = ManageMessageId.Error, @dialog = 1 });
                }
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                if (SiteContext.IsDialogPage)
                {
                    return RedirectToAction("Manage", new { @dialog = 1 });
                }
                return RedirectToAction("Manage");
            }

            if (SiteContext.IsDialogPage)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error, @dialog = 1 });
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (SiteContext.IsDialogPage)
                {
                    return RedirectToAction("Manage", new { @dialog = 1 });
                }
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult LogOffPublic(string redirect)
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            if (string.IsNullOrEmpty(redirect))
            {
                redirect = "/";
            }

            return RedirectToAction("Login", new { redirect = redirect });
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        public JsonResult GetAssingedRoles(string userid)
        {
            List<string> roles = new List<string>();
            if (userid != "undefined")
            {
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var uroles = UserManager.GetRoles(userid);
                foreach (var item in uroles)
                {
                    string asrole = db.AspNetRoles.Where(nm => nm.Name == item).FirstOrDefault().Id;
                    roles.Add(asrole);
                }
            }
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
            SiteContext.CurrentUser_SessionKey = null;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                if (SiteContext.IsDialogPage)
                {
                    return RedirectToAction("Index", "Dashboard", new { @dialog = 1 });
                }
                return RedirectToAction("Index", "Dashboard");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}