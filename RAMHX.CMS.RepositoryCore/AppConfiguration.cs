using Microsoft.AspNetCore.Mvc.Rendering;
using RAMHX.CMS.DataAccessCore;
using RAMHX.CMS.InfraCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RAMHX.CMS.RepositoryCore
{
    public class AppConfiguration
    {
        DatabaseEntities dataContext = new DatabaseEntities();

        #region Get configuration Items

        /// <summary>
        /// Get all configuration Items
        /// </summary>
        /// <returns>configuration item list</returns>
        public List<AppConfigs> GetAllGroupItems()
        {
            return dataContext.AppConfigs.ToList();
        }

        public List<AppConfigs> GetAllGroups()
        {
            return dataContext.AppConfigs.Where(gr => gr.ItemId == 0).ToList();
        }

        public int GetMaxItemId(int GroupID)
        {
            var Configuration = dataContext.AppConfigs.Where(grp => grp.GroupId == GroupID).OrderByDescending(u => u.ItemId).FirstOrDefault();
            return Configuration.ItemId;
        }

        /// <summary>
        /// Get all group types by Configuration id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AppConfigs> GetConfigurationItemsByConfigurationID(int id)
        {
            return dataContext.AppConfigs.Where(grp => grp.GroupId == id).ToList();
        }

        /// <summary>
        /// Get group types by Item Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AppConfigs> GetConfigurationItemsByItemID(int id)
        {
            return dataContext.AppConfigs.Where(grp => grp.ItemId == id).ToList();
        }


        public AppConfigs GetGroupItemByItemAndGroupID(int itemid, int groupId)
        {
            return dataContext.AppConfigs.Where(itm => itm.ItemId == itemid && itm.GroupId == groupId).FirstOrDefault();
        }

        public AppConfigs GetGroupItemByItemAndGroupID(string itemcode, int groupId)
        {
            return dataContext.AppConfigs.Where(itm => itm.ItemCode == itemcode && itm.GroupId == groupId).FirstOrDefault();
        }


        public static string GetAppSettings(string itemcode)
        {
            return SQLDataAccess.GetAppSettings(itemcode);
        }

        private List<SelectListItem> GetGroupItemSelectList(int groupId)
        {
            return GetConfigurationItemsByConfigurationID(groupId).Where(grp => grp.IsActive == true).Select(x => new SelectListItem() { Text = x.ItemName, Value = x.ItemId.ToString() }).ToList();
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
        public bool UpdateItem(AppConfigs model)
        {
            var item = dataContext.AppConfigs.Where(itm => itm.ItemId == model.ItemId && itm.GroupId == model.GroupId).FirstOrDefault();
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

        public bool DeleteItem(AppConfigs model)
        {
            var item = dataContext.AppConfigs.Where(itm => itm.ItemId == model.ItemId && itm.GroupId == model.GroupId).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            dataContext.AppConfigs.Remove(item);
            dataContext.SaveChanges();
            return true;
        }

        public void CreateNewItem(AppConfigs model, int groupID)
        {
            int maxId = 0;
            var grpId = dataContext.AppConfigs.Where(grp => grp.GroupId == model.GroupId).FirstOrDefault();
            if (grpId != null)
                maxId = GetMaxItemId(groupID);
            AppConfigs newmodel = new AppConfigs();
            newmodel.ItemId = model.ItemId;
            newmodel.GroupId = groupID;
            newmodel.ItemName = model.ItemName;
            newmodel.ShortDesc = model.ShortDesc;
            newmodel.ItemCode = model.ItemCode;
            newmodel.ItemDesc = model.ItemDesc;
            newmodel.IsActive = model.IsActive;
            dataContext.AppConfigs.Add(newmodel);
            dataContext.SaveChanges();
        }
        #endregion
    }
}
