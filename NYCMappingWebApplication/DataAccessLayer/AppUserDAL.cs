using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.DataAccessLayer
{
    public class AppUserDAL
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        public List<SelectListItem> GetAllUserRoles()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result = (from u in db.UserRoles
                      select new SelectListItem
                      {
                          Text = u.RoleName,
                          Value = u.UserRole_ID.ToString()
                      }).ToList();
            return result;
        }
        public AppUserInfo GetAppUserInfoByID(int User_ID)
        {
            AppUserInfo result = new AppUserInfo();
            result = (from u in db.Users
                      where u.User_ID == User_ID
                      select new AppUserInfo
                      {
                          Username = u.Username,
                          Role_ID = u.Role_ID,
                          IsDeleted = u.IsDeleted
                      }).FirstOrDefault();
            return result;
        }
    }
}