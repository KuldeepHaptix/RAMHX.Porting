using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace RAMHX.CMS.DataAccessCore
{
    public class SQLDataAccess
    {
        public static string GetAppSettings(string itemcode)
        {
            try
            {
                DatabaseEntities dataContext = new DatabaseEntities();
                var app = dataContext.AppConfigs.Where(itm => itm.ItemCode == itemcode && itm.GroupId == 1).FirstOrDefault();
                if (app != null)
                {
                    return app.ItemDesc;
                }
            }
            catch (Exception ex)
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Error(ex);
            }

            return string.Empty;
        }

        //DatabaseEntities dataContext = new DatabaseEntities();
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void TakeAutoDailyBackup(bool takeNow)
        {
           

            if (GetAppSettings("RAMHX.AllowAutoBack") != "true")
            {
                return;
            }

            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            try
            {
                var path = Path.GetDirectoryName(Path.Combine("/")) + "\\dbbackups\\" + DateTime.Now.ToString("DB_yyyyMMdd.bak");
                if (takeNow)
                {
                    path = Path.GetDirectoryName(Path.Combine("/")) + "\\dbbackups\\" + DateTime.Now.ToString("DB_yyyyMMdd_HHmmss.bak");
                }
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                if (!File.Exists(path))
                {
                    DatabaseEntities dataContext = new DatabaseEntities();
                    using (SqlCommand cmd = new SqlCommand("Backup database " + dataContext.Database + "  to disk='" + path + "'"))
                    {
                        //cmd.Connection = (SqlConnection)dataContext.Database.Connection;
                        // Open connection
                        //if (dataContext.Database.Connection.State != ConnectionState.Open)
                        //    dataContext.Database.Connection.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 150;
                        cmd.ExecuteNonQuery();
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Warn("TakeAutoDailyBackup Failed: ", ex);
            }
            return;
        }

        private string GetScript(string scriptPath)
        {
            string strScript;

            // Load script file 
            // using (System.IO.StreamReader objStreamReader = System.IO.File.OpenText(scriptPath)) 
            // http://support.Appleseedportal.net/jira/browse/RBP-693
            // to make it possible to have German umlauts or other special characters in the install_scripts
            using (var objStreamReader = new StreamReader(scriptPath, Encoding.Default))
            {
                strScript = objStreamReader.ReadToEnd();
                objStreamReader.Close();
            }

            return strScript + Environment.NewLine; // Append carriage for execute last command 
        }

        private List<string> ExecuteScript(string scriptPath)
        {

            var strScript = GetScript(scriptPath);
            logger.Info(string.Format("Executing Script '{0}'", scriptPath));
            var errors = new List<string>();

            // Subdivide script based on GO keyword
            var sqlCommands = Regex.Split(strScript, "\\sGO\\s", RegexOptions.IgnoreCase);
            DatabaseEntities dataContext = new DatabaseEntities();
            try
            {
                // Cycles command and execute them
                for (var s = 0; s <= sqlCommands.GetUpperBound(0); s++)
                {
                    var commandText = sqlCommands[s].Trim();

                    try
                    {
                        if (commandText.Length > 0)
                        {
                            // Open connection
                            //if (dataContext.Database.Connection.State != ConnectionState.Open)
                            //    dataContext.Database.Connection.Open();

                            //logger.Info("Executing: " + commandText.Replace("\n", " "));
                            using (var sqldbCommand = new SqlCommand())
                            {
                                //sqldbCommand.Connection = (SqlConnection)dataContext.Database.Connection;
                                sqldbCommand.CommandType = CommandType.Text;
                                sqldbCommand.CommandText = commandText;
                                sqldbCommand.CommandTimeout = 150;
                                sqldbCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add(string.Format("<p>{0}<br />{1}</p>", ex.Message, commandText));
                        logger.Warn("ExecuteScript Failed: " + commandText, ex);
                    }
                    finally
                    {
                        //if (dataContext.Database.Connection.State == ConnectionState.Open)
                        //{
                        //    dataContext.Database.Connection.Close();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                logger.Warn("ExecuteScript Failed: ", ex);

                var count = 0;

                while (ex.InnerException != null && count < 100)
                {
                    ex = ex.InnerException;
                    errors.Add(ex.Message);
                    count++;
                }
            }

            return errors;
        }

        public void ExecuteScript(CmsUpgradeHistory release)
        {
            DatabaseEntities dataContext = new DatabaseEntities();
            if (release.Script.ToLower() == "version.1.0.0.0.sql" && !HasInitialDB())
            {
                var errors = ExecuteScript(release.ScriptFullPath);
                if (errors.Count == 0)
                {
                    //dataContext.Database.Connection.Open();
                    release.InstalledDate = DateTime.Now;
                    dataContext.CmsUpgradeHistory.Add(release);
                    dataContext.SaveChanges();
                   // dataContext.Database.Connection.Close();
                    logger.Info("Upgraded History Table into database for " + release.Script);
                }
            }
            else
            {
                var existUpgrade = dataContext.CmsUpgradeHistory.FirstOrDefault(rel => rel.Script == release.Script);
                if (existUpgrade == null)
                {
                    var errors = ExecuteScript(release.ScriptFullPath);
                    if (errors.Count == 0)
                    {
                        // dataContext.Database.Connection.Open();
                        release.InstalledDate = DateTime.Now;
                        dataContext.CmsUpgradeHistory.Add(release);
                        dataContext.SaveChanges();
                        // dataContext.Database.Connection.Close();
                        logger.Info("Upgraded History Table into database for " + release.Script);
                    }
                }
            }

        }

        private bool HasInitialDB()
        {
            try
            {
                DatabaseEntities dataContext = new DatabaseEntities();
                var existUpgrade = dataContext.CmsUpgradeHistory.First();
                return true;
            }
            catch { }
            return false;
        }
    }
}
