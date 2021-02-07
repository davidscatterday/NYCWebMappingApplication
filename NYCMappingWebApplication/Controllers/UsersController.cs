using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.Controllers
{
    public class UsersController : Controller
    {
        UserDAL userDAL = new UserDAL();
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        // GET: Users
        public ActionResult UserTabs()
        {
            if (User.Identity.Name != "david@scatterdayassociates.com" && User.Identity.Name != "pavlejovanov@gmail.com")
            {
                return RedirectToAction("Login", "Account");
            }
            var data = userDAL.GetUserTabs();
            return View(data);
        }
        // GET: Users/AddUserTab
        public ActionResult AddUserTab()
        {
            if (User.Identity.Name != "david@scatterdayassociates.com" && User.Identity.Name != "pavlejovanov@gmail.com")
            {
                return RedirectToAction("Login", "Account");
            }
            AddUserTabs data = new AddUserTabs();

            return View(data);
        }

        // POST: /Users/AddUserTab
        [HttpPost]
        public ActionResult AddUserTab(AddUserTabs data)
        {
            if (db.User_Tabs.Any(w => w.Username == data.Username))
            {
                TempData["ErrorMessage"] = "This username already exists";
                return View(data);
            }
            if (ModelState.IsValid)
            {
                User_Tabs user_tab = new User_Tabs()
                {
                    Username = data.Username,
                    TabIDs = userDAL.GetTabIDsByNames(data.TabIDs)
                };
                db.User_Tabs.Add(user_tab);
                db.SaveChanges();
                TempData["InfoMessage"] = "New user privileges has been successfully added";
                return RedirectToAction("UserTabs");
            }
            
            return View(data);
        }

        // GET: Users/EditUserTab
        public ActionResult EditUserTab(int ID)
        {
            if (User.Identity.Name != "david@scatterdayassociates.com" && User.Identity.Name != "pavlejovanov@gmail.com")
            {
                return RedirectToAction("Login", "Account");
            }
            var record = db.User_Tabs.Find(ID);
            AddUserTabs data = new AddUserTabs()
            {
                ID = ID,
                Username = record.Username,
                TabIDs = userDAL.GetTabNamesByIDs(record.TabIDs)
            };

            return View(data);
        }

        // POST: /Users/EditUserTab
        [HttpPost]
        public ActionResult EditUserTab(AddUserTabs data)
        {
            if (ModelState.IsValid)
            {
                User_Tabs editedUser_Tab = db.User_Tabs.Find(data.ID);
                if (data.Username != editedUser_Tab.Username && db.User_Tabs.Any(w => w.Username == data.Username))
                {
                    TempData["ErrorMessage"] = "This username already exists";
                    return View(data);
                }
                editedUser_Tab.Username = data.Username;
                editedUser_Tab.TabIDs = userDAL.GetTabIDsByNames(data.TabIDs);
                db.Entry(editedUser_Tab).State = EntityState.Modified;
                db.SaveChanges();
                TempData["InfoMessage"] = "User privileges has been successfully edited";
                return RedirectToAction("UserTabs");
            }
            
            return View(data);
        }

        // GET: /Users/DeleteUserTab
        public ActionResult DeleteUserTab(int ID)
        {
            if (User.Identity.Name != "david@scatterdayassociates.com" && User.Identity.Name != "pavlejovanov@gmail.com")
            {
                return RedirectToAction("Login", "Account");
            }
            var user_tab = db.User_Tabs.Find(ID);
            if (user_tab == null)
            {
                return HttpNotFound();
            }
            AddUserTabs data = new AddUserTabs()
            {
                ID = ID,
                Username = user_tab.Username,
                TabIDs = userDAL.GetTabNamesByIDs(user_tab.TabIDs)
            };
            return View(data);
        }

        // POST: /Users/DeleteUserTab
        [HttpPost, ActionName("DeleteUserTab")]
        public ActionResult DeleteConfirmed(int ID)
        {
            var user_tab = db.User_Tabs.Find(ID);
            db.User_Tabs.Remove(user_tab);
            db.SaveChanges();
            TempData["InfoMessage"] = "User privileges has been successfully deleted";
            return RedirectToAction("UserTabs");
        }

        public JsonResult GetAllTabs(string term)
        {
            var TabsList = userDAL.GetAllTabs(term);
            return Json(new { TabsList }, JsonRequestBehavior.AllowGet);
        }

    }
}