using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using RAMHX.CMS.DataAccessCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static RAMHX.CMS.RepositoryCore.CacheRepository;

namespace RAMHX.CMS.RepositoryCore
{
    public class RenderHtmlRepository : BaseRepository
    {
        public RenderHtmlRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        public string HtmlOut { get; set; }

        public string GetContent(string htmlModuleCode, ControllerContext context, bool second = false)
        {
            CacheRepository cr = new CacheRepository(this.Context);
            var mainHtml = this.dataContext.HtmlModule.FirstOrDefault(h => h.HtmlModuleCode == htmlModuleCode);
            if (mainHtml != null)
            {
                var data = cr.Get(mainHtml.HtmlmoduleId.ToString(), CacheTypes.HtmlModule);
                if (data != null)
                {
                    return data.ToString();
                }

            }
            bool addToCache = true;
            string html = string.Empty;
            try
            {
                if (mainHtml != null && !string.IsNullOrEmpty(mainHtml.HtmlModuleHtml))
                {
                    if (!second)
                    {
                        HtmlOut = mainHtml.HtmlModuleHtml;
                    }
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    if (!string.IsNullOrEmpty(mainHtml.HtmlModuleHtml))
                    {
                        doc.LoadHtml(mainHtml.HtmlModuleHtml);//TODO; changed by Regular expression
                    }

                    var items = doc.DocumentNode.SelectNodes("//ramhx");
                    if (items != null && items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            string htmlcd = string.Empty;
                            string viewPath = string.Empty;
                            try
                            {
                                if (item.Attributes["htmlcode"] != null && !string.IsNullOrEmpty(item.Attributes["htmlcode"].Value))
                                {
                                    htmlcd = item.Attributes["htmlcode"].Value;
                                }
                                else if (item.Attributes["viewpath"] != null && !string.IsNullOrEmpty(item.Attributes["viewpath"].Value))
                                {
                                    viewPath = item.Attributes["viewpath"].Value;
                                }

                                if (!string.IsNullOrEmpty(htmlcd) && htmlcd != htmlModuleCode)
                                {
                                    var sbhtml = this.dataContext.HtmlModule.FirstOrDefault(h => h.HtmlModuleCode == htmlcd);
                                    if (sbhtml != null)
                                    {
                                        HtmlOut = HtmlOut.Replace(item.OuterHtml, sbhtml.HtmlModuleHtml);
                                        this.GetContent(sbhtml.HtmlModuleCode, context, true);
                                    }
                                    else
                                    {
                                        HtmlOut = HtmlOut.Replace(item.OuterHtml, string.Empty);
                                        logger.Warn("Html Code -'" + htmlcd + "' does not exist.");
                                    }
                                    addToCache = false;
                                }
                                else if (!string.IsNullOrEmpty(viewPath))
                                {
                                    if (viewPath.Contains(".cshtml"))
                                    {
                                        doc.LoadHtml(RenderViewToString(context, viewPath));
                                    }
                                    else
                                    {
                                        
                                        //doc.Load( )
                                    }

                                    HtmlOut = HtmlOut.Replace(item.OuterHtml, doc.DocumentNode.OuterHtml);
                                    addToCache = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Html Code items- Error occured ", ex);
                                addToCache = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Html Code - Error occured ", ex);
                addToCache = false;
                return string.Empty;
            }

            if (addToCache)
            {
                if (!string.IsNullOrEmpty(HtmlOut))
                {
                    CacheRepository.Add(mainHtml.HtmlmoduleId.ToString(), HtmlOut, CacheTypes.HtmlModule);
                    return HtmlOut;
                }
                else
                {
                    if (mainHtml != null && !string.IsNullOrEmpty(mainHtml.HtmlModuleHtml))
                    {
                        CacheRepository.Add(mainHtml.HtmlmoduleId.ToString(), HtmlOut, CacheTypes.HtmlModule);
                        return mainHtml.HtmlModuleHtml;
                    }
                }

                CacheRepository.Add(mainHtml.HtmlmoduleId.ToString(), HtmlOut, CacheTypes.HtmlModule);
            }

            return HtmlOut;
        }


        //public string GetViewinHtml(string viewName)
        //{
        //    var controller = ViewRenderer.CreateController<GenericController>();

        //    string html = ViewRenderer.RenderPartialView(
        //                                "~/views/shared/Error.cshtml",
        //                                model,
        //                                controller.ControllerContext);

        //    HttpContext.Current.Server.ClearError();


        //    ViewData.Model = model;

        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
        //                                                                 viewName);
        //        var viewContext = new ViewContext(ControllerContext, viewResult.View,
        //                                     ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);
        //        viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}

        public  String RenderViewToString(ControllerContext contextc, String viewPath, object model = null)
        {
            if (contextc == null)
            {
                //var ctrlFactory = ControllerBuilder.Current.GetControllerFactory();
                //var ctrl = ctrlFactory.CreateController(HttpContext.Current.Request.RequestContext, HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString()) as Controller;
                //context = new ControllerContext(HttpContext.Current.Request.RequestContext, ctrl);
                contextc = new ControllerContext();
                
            }

            //contextc.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
               // var viewResult = IViewEngine.FindView(contextc, viewPath, false);
                var viewContext = new ViewContext();
                //viewResult.View.RenderAsync(viewContext);
               // viewResult.Success(contextc, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public List<HtmlModule> GetAllHtmlModules()
        {
            return this.dataContext.HtmlModule.ToList();
        }
    }
}
