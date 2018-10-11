using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Repository;
using RAMHX.CMS.Web.App_Start;
using RAMHX.CMS.Web.Areas.Admin.Scheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml;
using System.Xml.Linq;

namespace RAMHX.CMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly object startLoading = new object();
        private static bool LoadedApplication = false;

        public event Action ApplicationStart;

        public static string WebsiteRoot = "";

        public static void InitApplication()
        {
            lock (startLoading)
            {
                if (!LoadedApplication || RAMHX.CMS.Web.SiteContext.Pages == null || RAMHX.CMS.Web.SiteContext.Pages.Count < 1)
                {
                    log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    logger.Info("RAMHX Init Application...");

                    LoadedApplication = false;

                    WebsiteRoot = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.Port != 80 ? ":" + HttpContext.Current.Request.Url.Port : "") + "/";

                    bool databaseInslled = RAMHX.CMS.Infra.Common.HasCorrectConnectionString;

                    if (databaseInslled)
                    {
                        Installer.Run();
                        PageRepository.RefreshPages();
                    }

                    JobScheduler.Start();
                    LoadedApplication = true;
                }
            }
        }

        public static bool IsUnderMaintanceOrCommingSoonEnabled()
        {
            if (HttpContext.Current != null)
            {
                if (!HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains("/admin/") 
                    && !HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains("undermaintanance")
                    && !HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains("commingsoon")
                    && !HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains("preview=1")
                    )
                {
                    
                    if (AppConfiguration.GetAppSettings("RAMHX.Enabled.UnderMaintanance") == "1")
                    {
                        HttpContext.Current.Response.Redirect("/UnderMaintanance.html", true);
                        return true;
                    }
                    else if (AppConfiguration.GetAppSettings("RAMHX.Enabled.CommingSoon") == "1")
                    {
                        HttpContext.Current.Response.Redirect("/CommingSoon.html", true);
                        return true;
                    }
                }
            }
           
            return false;
        }

        public void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Server.MapPath("~/Web.config")));
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("RAMHX Starting...");

            LoadActions(Server.MapPath("/App_Config/application_start_before.xml"));

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (ApplicationStart != null)
                ApplicationStart();

            LoadActions(Server.MapPath("/App_Config/application_start_after.xml"));

            bool databaseInslled = RAMHX.CMS.Infra.Common.HasCorrectConnectionString;
            if (databaseInslled)
            {
                Installer.Run();
                PageRepository.RefreshPages();
            }

            SQLDataAccess.TakeAutoDailyBackup(false);
        }

        private static void LoadActions(string beforeApp)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("LoadActions Started");
            if (File.Exists(beforeApp))
            {
                XDocument source = XDocument.Load(beforeApp);
                foreach (XElement referance in source.Root.Elements("referance"))
                {
                    if (referance.Attribute("assembly") != null && referance.Attribute("type") != null)
                    {
                        string assemblyDLL = referance.Attribute("assembly").Value;
                        string typeClass = referance.Attribute("type").Value;
                        if (!string.IsNullOrEmpty(typeClass) && !string.IsNullOrEmpty(assemblyDLL))
                        {
                            Assembly assembly = Assembly.LoadFrom(HttpContext.Current.Server.MapPath("/bin/" + assemblyDLL));
                            Type type = assembly.GetType(typeClass);
                            try
                            {
                                RAMHX.CMS.Infra.IApplicationStart instanceOfMyType = (RAMHX.CMS.Infra.IApplicationStart)Activator.CreateInstance(type);
                                instanceOfMyType.Execute();
                                logger.Info("LoadActions - " + assemblyDLL + "," + typeClass + " - Success!");
                            }
                            catch(Exception ex) {
                                logger.Error("LoadActions - " + assemblyDLL + "," + typeClass + " - Fail!", ex);
                            }
                        }
                    }
                }
            }
            logger.Info("LoadActions Ended");
        }

        public void Application_Error(object sender, EventArgs e)
        {
            

            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            // An error has occured on a .Net page.
            var serverError = Server.GetLastError() as HttpException;
            if (null != serverError)
            {
                int errorCode = serverError.GetHttpCode();

                if (404 == errorCode)
                {
                    Server.ClearError();

                    //if (HttpContext.Current.Request.Url.PathAndQuery != "/")
                    //    logger.Info("PageNotfound for - " + serverError.Message, serverError);
                    if (!HttpContext.Current.Request.Url.PathAndQuery.ToLower().Split('?').GetValue(0).ToString().Contains("."))
                    {
                        if (RAMHX.CMS.Infra.Common.HasCorrectConnectionString)
                            HttpContext.Current.Response.Redirect(Repository.PageRepository.PageNotFound(), true);
                        else
                            HttpContext.Current.Response.Redirect("/Admin/Pages/PageNotFound", true);
                    }
                }
                else
                {
                    logger.Info("Application_Error for - " + serverError.Message, serverError);
                }
            }
            else
            {
                try
                {
                    System.Web.HttpRuntime.UnloadAppDomain();
                    logger.Info("RAMHX - Restarted! - System.Web.HttpRuntime.UnloadAppDomain()");
                }
                catch (Exception ex)
                {
                    logger.Error("Application_Error > try > catch - " + ex.Message, ex);
                }
            }
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {

            InitApplication();

            if (MvcApplication.IsUnderMaintanceOrCommingSoonEnabled())
            {
                return;
            }

            //Create an instance of the application that has raised the event
            HttpApplication httpApplication = sender as HttpApplication;

            //Safety check for the variable httpApplication if it is not null
            if (httpApplication != null && RAMHX.CMS.Infra.Common.HasCorrectConnectionString)
            {

                //get the request path - request path is    something you get in
                //the url
                string requestPath = httpApplication.Context.Request.Url.PathAndQuery.Split('?')[0].ToLower();

                //variable for translation path
                string translationPath = "";
                var filePath = httpApplication.Context.Server.MapPath(requestPath);

                List<string> lstAvoidRequests = new List<string>() {
                    "/admin",
                    "/bundles",
                    "/__browserlink"

                };

                //301 redirection
                string redirectUrl = new RedirectsRepository().Get301RedirectUrl(requestPath);
                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    httpApplication.Context.Response.Redirect(redirectUrl, true);
                    return;
                }

                if (lstAvoidRequests.Count(re => requestPath.StartsWith(re)) == 0 && !System.IO.File.Exists(filePath))
                {
                    if (requestPath != "/" && !string.IsNullOrEmpty(requestPath))
                    {
                        var curpage = Repository.PageRepository.GetPageByUrl(requestPath);
                        if (curpage != null)
                        {
                            HttpContext.Current.Items["CurrentPage"] = curpage;
                            translationPath = "/Admin/Pages/RenderContent?id=" + curpage.PageID;
                            if (httpApplication.Context.Request.Url.PathAndQuery.Contains("?"))
                            {
                                translationPath += "&" + httpApplication.Context.Request.Url.PathAndQuery.Split('?')[1];
                            }
                        }
                    }
                    else
                    {
                        var defaultPage = RAMHX.CMS.Web.SiteContext.Pages.Where(page => page.ParentPageID == null).OrderBy(page => page.PageOrder).FirstOrDefault();
                        if (defaultPage != null)
                        {
                            HttpContext.Current.Items["CurrentPage"] = defaultPage;
                            translationPath = "/Admin/Pages/RenderContent?id=" + defaultPage.PageID;
                            if (httpApplication.Context.Request.Url.PathAndQuery.Contains("?"))
                            {
                                translationPath += "&" + httpApplication.Context.Request.Url.PathAndQuery.Split('?')[1];
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(translationPath))
                    httpApplication.Context.RewritePath(translationPath);
            }

            string requestPathDirect = this.Request.Url.PathAndQuery.Split('?')[0].ToLower();
            if (!RAMHX.CMS.Infra.Common.HasCorrectConnectionString && !requestPathDirect.Contains("/admin/installer") && (!RAMHX.CMS.Infra.Common.ValidateUrl(requestPathDirect)))
            {
                if (httpApplication != null)
                {
                    string requestPath = httpApplication.Context.Request.Url.PathAndQuery.Split('?')[0].ToLower();
                    var filePath = httpApplication.Context.Server.MapPath(requestPath);
                    if (System.IO.File.Exists(filePath))
                    {
                        return;
                    }
                }
                log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Info("Redirect to Fresh Installation");
                this.Response.Redirect("/admin/Installer", true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Application_End()
        {

            HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime",
                                                                                            BindingFlags.NonPublic
                                                                                            | BindingFlags.Static
                                                                                            | BindingFlags.GetField,
                                                                                            null,
                                                                                            null,
                                                                                            null);
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            if (runtime == null)
            {
                logger.Info(String.Format("Shutdown RAMHX"));
                return;
            }
            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
                                                                             BindingFlags.NonPublic
                                                                             | BindingFlags.Instance
                                                                             | BindingFlags.GetField,
                                                                             null,
                                                                             runtime,
                                                                             null);

            string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
                                                                           BindingFlags.NonPublic
                                                                           | BindingFlags.Instance
                                                                           | BindingFlags.GetField,
                                                                           null,
                                                                           runtime,
                                                                           null);

            logger.Info(String.Format("Shutdown RAMHX\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}",
                                         shutDownMessage,
                                         shutDownStack));
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection.ClearAllPools();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started  
            Application.Lock();
            if (HttpContext.Current.Application["TotalOnlineUsers"] != null)
                Application["TotalOnlineUsers"] = (int)Application["TotalOnlineUsers"] + 1;
            else
                Application["TotalOnlineUsers"] = 1;
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.   
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer   
            // or SQLServer, the event is not raised.  
            Application.Lock();
            Application["TotalOnlineUsers"] = (int)Application["TotalOnlineUsers"] - 1;
            Application.UnLock();
        }
    }
}
