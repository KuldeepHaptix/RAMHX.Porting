using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RAMHX.CMS.Repository
{
    public class AppConfiguration
    {
        DatabaseEntities dataContext = new DatabaseEntities();

        #region Get configuration Items

        /// <summary>
        /// Get all configuration Items
        /// </summary>
        /// <returns>configuration item list</returns>
        public List<app_Configs> GetAllGroupItems()
        {
            return dataContext.app_Configs.ToList();
        }

        public List<app_Configs> GetAllGroups()
        {
            return dataContext.app_Configs.Where(gr => gr.ItemId == 0).ToList();
        }

        public int GetMaxItemId(int GroupID)
        {
            var Configuration = dataContext.app_Configs.Where(grp => grp.GroupId == GroupID).OrderByDescending(u => u.ItemId).FirstOrDefault();
            return Configuration.ItemId;
        }

        /// <summary>
        /// Get all group types by Configuration id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<app_Configs> GetConfigurationItemsByConfigurationID(int id)
        {
            return dataContext.app_Configs.Where(grp => grp.GroupId == id).ToList();
        }

        /// <summary>
        /// Get group types by Item Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<app_Configs> GetConfigurationItemsByItemID(int id)
        {
            return dataContext.app_Configs.Where(grp => grp.ItemId == id).ToList();
        }


        public app_Configs GetGroupItemByItemAndGroupID(int itemid, int groupId)
        {
            return dataContext.app_Configs.Where(itm => itm.ItemId == itemid && itm.GroupId == groupId).FirstOrDefault();
        }

        public app_Configs GetGroupItemByItemAndGroupID(string itemcode, int groupId)
        {
            return dataContext.app_Configs.Where(itm => itm.ItemCode == itemcode && itm.GroupId == groupId).FirstOrDefault();
        }


        public static string GetAppSettings(string itemcode)
        {
            return SQLDataAccess.GetAppSettings(itemcode);
        }

        private List<SelectListItem> GetGroupItemSelectList(int groupId)
        {
            return GetConfigurationItemsByConfigurationID(groupId).Where(grp => grp.IsActive).Select(x => new SelectListItem() { Text = x.ItemName, Value = x.ItemId.ToString() }).ToList();
        }

        public List<SelectListItem> GetFieldTypeSelectList()
        {
            return GetGroupItemSelectList((int)Enums.AppConfigs.Groups.FieldTypes);
        }

        #endregion

        #region Operations

        /// <summary>
        /// Update Email template
        /// </summary>
        /// <param name="model">group model</param>
        /// <returns>yes/no</returns>
        public bool UpdateItem(app_Configs model)
        {
            var item = dataContext.app_Configs.Where(itm => itm.ItemId == model.ItemId && itm.GroupId == model.GroupId).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            item.ItemName = model.ItemName;
            item.ItemCode = model.ItemCode;
            item.ShortDesc = model.ShortDesc;
            item.ItemDesc = model.ItemDesc;
            item.IsActive = model.IsActive;
            dataContext.SaveChanges();
            return true;
        }

        public bool DeleteItem(app_Configs model)
        {
            var item = dataContext.app_Configs.Where(itm => itm.ItemId == model.ItemId && itm.GroupId == model.GroupId).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            dataContext.app_Configs.Remove(item);
            dataContext.SaveChanges();
            return true;
        }

        public void CreateNewItem(app_Configs model, int groupID)
        {
            int maxId = 0;
            var grpId = dataContext.app_Configs.Where(grp => grp.GroupId == model.GroupId).FirstOrDefault();
            if (grpId != null)
                maxId = GetMaxItemId(groupID);
            app_Configs newmodel = new app_Configs();
            newmodel.ItemId = model.ItemId;
            newmodel.GroupId = groupID;
            newmodel.ItemName = model.ItemName;
            newmodel.ShortDesc = model.ShortDesc;
            newmodel.ItemCode = model.ItemCode;
            newmodel.ItemDesc = model.ItemDesc;
            newmodel.IsActive = model.IsActive;
            dataContext.app_Configs.Add(newmodel);
            dataContext.SaveChanges();
        }
        #endregion
    }
}
