namespace RAMHX.CMS.Web
{
    using System;
    using System.Web;
    using System.Collections;
    using System.Linq;
    using Infra;
    using System.Collections.Generic;
    using Repository;

    /// <summary>
    /// Summary description for URLRewriter
    /// </summary>
    public class URLRewriter : IHttpModule
    {
        #region IHttpModule Members

        /// <summary>
        /// Dispose method for the class
        /// If you have any unmanaged resources to be disposed
        /// free them or release them in this method
        /// </summary>
        public void Dispose()
        {
            //not implementing this method
            //for this example
        }

        /// <summary>
        /// Initialization of the http application instance
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            //if (Common.HasCorrectConnectionString)
            //    context.BeginRequest += new EventHandler(context_BeginRequest);
        }
        /// <summary>
        /// Event handler of instance begin request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void context_BeginRequest(object sender, EventArgs e)
        {
            MvcApplication.InitApplication();

            //Create an instance of the application that has raised the event
            HttpApplication httpApplication = sender as HttpApplication;
            if (MvcApplication.IsUnderMaintanceOrCommingSoonEnabled())
            {
                return;
            }

            //Safety check for the variable httpApplication if it is not null
            if (httpApplication != null)
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
        }


        #endregion
    }
}