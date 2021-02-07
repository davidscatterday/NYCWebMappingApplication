using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.DataAccessLayer
{
    public class UserDAL
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        public List<UserTabs> GetUserTabs()
        {
            List<UserTabs> result = new List<UserTabs>();
            result = (from ut in db.User_Tabs
                      select new UserTabs
                      {
                          ID = ut.ID,
                          Username = ut.Username,
                          TabIDs = ut.TabIDs
                      }).ToList();
            foreach (UserTabs item in result)
            {
                var tabIDs = item.TabIDs.Split(',').Select(s => int.Parse(s));
                var tabNames = db.Tabs.Where(k => tabIDs.Contains(k.TabID)).Select(p => p.TabName); ;
                item.TabNames = string.Join(",", tabNames);
            }
            return result;
        }

        public List<Select2DTO> GetAllTabs(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult = (from d in db.Tabs
                            where d.TabName.Contains(term) && d.TabID != 1
                            select new Select2DTO()
                            {
                                id = d.TabName,
                                text = d.TabName
                            }).Take(30).ToList();
            return returnResult;
        }

        public string GetTabIDsByNames(string TabNames)
        {
            var tabNames = TabNames.Split(',').ToList();
            var tabIDs = db.Tabs.Where(k => tabNames.Contains(k.TabName)).Select(p => p.TabID).ToList();
            string returnResult = string.Join(",", tabIDs);
            return returnResult;
        }

        public string GetTabNamesByIDs(string TabIDs)
        {
            var tabIDs = TabIDs.Split(',').Select(s => int.Parse(s));
            var tabNameIDs = db.Tabs.Where(k => tabIDs.Contains(k.TabID)).Select(p => p.TabName).ToList();
            string returnResult = string.Join(",", tabNameIDs);
            return returnResult;
        }

    }
}