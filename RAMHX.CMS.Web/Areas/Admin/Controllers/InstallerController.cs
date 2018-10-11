using RAMHX.CMS.Web.App_Start;
using RAMHX.CMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    public class InstallerController : Controller
    {
        // GET: Admin/Installer
        public ActionResult Index()
        {
            if (RAMHX.CMS.Infra.Common.HasCorrectConnectionString)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(new InstallerModel());
            }
        }

        [HttpPost]
        public ActionResult Index(InstallerModel serverData)
        {
            if (!VerifyDB(serverData))
            {
                ModelState.AddModelError(string.Empty, "Invalid Database informatioin");
                return View(new InstallerModel());
            }

            if(RAMHX.CMS.Infra.Common.ValidateConnectionString(GetDatabaseConnectionString(serverData)))
            {
                UpdateConfig(serverData);
                Installer.Run();
                //return Json(new {status = "success", message = "Installed successfully", code = "1" });

                return RedirectToAction("Login", "Account");
            }
            else
            {
                //return Json(new { status = "error", message = "Invalid Database informatioin", code = "2" });
                ModelState.AddModelError(string.Empty, "Invalid Database informatioin");

                return View(new InstallerModel());
            }
        }

        private string GetDatabaseConnectionString(InstallerModel serverData)
        {
            //<add name="DefaultConnection" connectionString="Data Source=HAPTIX-BRD2\SQLEXPRESS;Initial Catalog=RAMHX.v2;Integrated Security=false;user id=sa;password=123;" providerName="System.Data.SqlClient" />
            return String.Format("Data Source={0};Initial Catalog={1};Integrated Security=false;user id={2};password={3};", serverData.SQLServer, serverData.Database, serverData.UserName, serverData.Password);
        }
        private bool VerifyDB(InstallerModel serverData)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetMasterDatabaseConnectionString(serverData)))
                {
                    conn.Open(); // throws if invalid
                    SqlCommand cmd = new SqlCommand("select count(*) from sysdatabases where name = '"+ serverData.Database +"'");
                    cmd.Connection = conn;
                    var count = Convert.ToInt16(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        cmd.CommandText = "CREATE DATABASE " + serverData.Database;
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
                //                    return !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
            catch(Exception ex)
            {
                return false;
            }
            // select count(*) from sysdatabases where name = '1'
        }
        private string GetMasterDatabaseConnectionString(InstallerModel serverData)
        {
            //<add name="DefaultConnection" connectionString="Data Source=HAPTIX-BRD2\SQLEXPRESS;Initial Catalog=RAMHX.v2;Integrated Security=false;user id=sa;password=123;" providerName="System.Data.SqlClient" />
            return String.Format("Data Source={0};Initial Catalog={1};Integrated Security=false;user id={2};password={3};", serverData.SQLServer, "master", serverData.UserName, serverData.Password);
        }

        private string GetEntityFwString(InstallerModel serverData)
        {
            //<add name="DatabaseEntities" connectionString="metadata=res://*/DataAccessModel.csdl|res://*/DataAccessModel.ssdl|res://*/DataAccessModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=HAPTIX-BRD2\SQLEXPRESS;initial catalog=RAMHX.v2;user id=sa;password=123;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
            return String.Format("metadata=res://*/DataAccessModel.csdl|res://*/DataAccessModel.ssdl|res://*/DataAccessModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source={0};Initial Catalog={1};user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework\"", serverData.SQLServer, serverData.Database, serverData.UserName, serverData.Password);
        }

        [HttpPost]
        public JsonResult Install(InstallerModel serverData)
        {
            if (RAMHX.CMS.Infra.Common.ValidateConnectionString(GetDatabaseConnectionString(serverData)))
            {
                UpdateConfig(serverData);
                Installer.Run();
                return Json("{response:'success'}");
            }
            else
            {
                return Json("{response:'failed', error:'Invalid database information'}");
            }
            
        }

        public void UpdateConfig(InstallerModel serverData)
        {
            var doc = new XmlDocument();

            doc.PreserveWhitespace = true;

            var configFile = ControllerContext.HttpContext.Server.MapPath("~/web.config");

            doc.Load(configFile);
            var dirty = false;

            // for Appleseed 2.0
            //var ns = new XmlNamespaceManager(doc.NameTable);
            //ns.AddNamespace("x", "http://schemas.microsoft.com/.NetConfiguration/v2.0");

            var connectionStrings = doc.SelectNodes("/configuration/connectionStrings/*");
            if (connectionStrings != null)
            {
                foreach (XmlNode connString in connectionStrings)
                {
                    if (connString.Name != "add")
                    {
                        continue;
                    }

                    var attrName = connString.Attributes["name"];
                    if (attrName == null)
                    {
                        continue;
                    }

                    switch (attrName.Value)
                    {
                        case "DefaultConnection":
                            {
                                var attrCSTRValue = connString.Attributes["connectionString"];
                                if (attrCSTRValue != null)
                                {
                                    attrCSTRValue.Value = this.GetDatabaseConnectionString(serverData);
                                    dirty = true;
                                }

                                var attrPVValue = connString.Attributes["providerName"];
                                if (attrPVValue != null)
                                {
                                    attrPVValue.Value = "System.Data.SqlClient";
                                    dirty = true;
                                }
                            }

                            break;

                        case "DatabaseEntities":
                            {
                                var attrMCSTRValue = connString.Attributes["connectionString"];
                                if (attrMCSTRValue != null)
                                {
                                    attrMCSTRValue.Value = this.GetEntityFwString(serverData);
                                    dirty = true;
                                }

                                var attrPVValue = connString.Attributes["providerName"];
                                if (attrPVValue != null)
                                {
                                    attrPVValue.Value = "System.Data.EntityClient";
                                    dirty = true;
                                }
                            }

                            break;
                    }
                }
            }

            if (dirty)
            {
                // Save the document to a file and auto-indent the output.
                var writer = new XmlTextWriter(configFile, Encoding.UTF8) { Formatting = Formatting.Indented };
                doc.Save(writer);
                writer.Flush();
                writer.Close();

                ConfigurationManager.RefreshSection("connectionStrings");
            }
        }

        public ActionResult AlreadyInstalled()
        {
            return View();
        }
    }
}