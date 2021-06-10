using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Helpers;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.Controllers
{
    public class AppUsersController : Controller
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        MainDAL mainDAL = new MainDAL();
        AppUserDAL appUserDAL = new AppUserDAL();
        // GET: AppUsers/Preview
        public ActionResult Preview()
        {
            if (!(GlobalVariables.GetFromCookie("NYCUser", "IsAdmin") == "True"))
                return RedirectToAction("Login", "AppUsers");
            var data = db.Users.OrderBy(w => w.Username).ToList();

            return View(data);
        }

        // GET: AppUsers/Add
        public ActionResult Add()
        {
            if (!(GlobalVariables.GetFromCookie("NYCUser", "IsAdmin") == "True"))
                return RedirectToAction("Login", "AppUsers");
            AppUserInfo data = new AppUserInfo();
            data.Password = GlobalVariables.InitPassword;
            data.RoleNames = appUserDAL.GetAllUserRoles();

            return View(data);
        }

        // POST: /AppUsers/Add
        [HttpPost]
        public ActionResult Add(AppUserInfo data)
        {
            data.RoleNames = appUserDAL.GetAllUserRoles();
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.Users.Any(w => w.Username.ToLower() == data.Username.ToLower()))
                    {
                        TempData["ErrorMessage"] = "This username is taken. Please choose other one";
                        return View(data);
                    }
                    else
                    {
                        User _user = new User
                        {
                            Username = data.Username,
                            Password = mainDAL.CalculateMD5Hash(data.Password),
                            Role_ID = data.Role_ID,
                            IsDeleted = false
                        };
                        db.Users.Add(_user);
                        db.SaveChanges();
                        TempData["InfoMessage"] = "New user has been successfully added";
                        return RedirectToAction("Preview");
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException != null ? "Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException.Message : "Message: " + ex.Message;
                    mainDAL.RecordInLogger("ERROR", "Add User", message, "", "");
                    TempData["ErrorMessage"] = "Some error happens and the user is not added. Contact the Admin.";
                    return View(data);
                }
            }

            return View(data);
        }

        // GET: /AppUsers/Edit/5
        public ActionResult Edit(int? User_ID)
        {
            if (!(GlobalVariables.GetFromCookie("NYCUser", "IsAdmin") == "True"))
                return RedirectToAction("Login", "AppUsers");
            if (User_ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUserInfo data = appUserDAL.GetAppUserInfoByID(User_ID.Value);
            data.RoleNames = appUserDAL.GetAllUserRoles();

            return View(data);
        }
        // POST: /AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(AppUserInfo data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User _user = db.Users.Find(data.User_ID);
                    _user.Role_ID = data.Role_ID;
                    _user.IsDeleted = data.IsDeleted;

                    db.Entry(_user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["InfoMessage"] = "The user has been successfully edited";
                    return RedirectToAction("Preview");
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException != null ? "Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException.Message : "Message: " + ex.Message;
                    mainDAL.RecordInLogger("ERROR", "Edit Admin Panel User", message, "", "");
                    TempData["ErrorMessage"] = "Some error happens and the user is not edited. Contact the Admin.";
                    return View(data);
                }
            }

            return View(data);
        }

        // GET: AppUsers/ChangePassword
        public ActionResult ChangePassword()
        {
            if (!(GlobalVariables.GetFromCookie("NYCUser", "IsLogged") == "True"))
                return RedirectToAction("Login", "AppUsers");
            ChangePasswordInfo data = new ChangePasswordInfo();

            return View(data);
        }

        // POST: /AppUsers/ChangePasswordInfo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordInfo data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string MD5_OldPassword = mainDAL.CalculateMD5Hash(data.OldPassword);
                    string myCookieUsername = GlobalVariables.GetFromCookie("NYCUser", "Username");
                    User _user = db.Users.Where(w => w.Username == myCookieUsername && w.Password == MD5_OldPassword).FirstOrDefault();
                    if (_user != null)
                    {
                        _user.Password = mainDAL.CalculateMD5Hash(data.NewPassword);
                        db.Entry(_user).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Your old password is not correct";
                        return View(data);
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException != null ? "Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException.Message : "Message: " + ex.Message;
                    mainDAL.RecordInLogger("ERROR", "Change Password", message, "", "");
                    TempData["ErrorMessage"] = "Some error happens and your password is not changed. Contact the Admin.";
                    return View(data);
                }
            }

            return View(data);
        }

        // GET: AppUsers/Login
        public ActionResult Login()
        {
            AppUserLoginInfo data = new AppUserLoginInfo();

            return View(data);
        }

        // POST: /AppUsers/Login
        [HttpPost]
        public ActionResult Login(string command, AppUserLoginInfo data)
        {
            if (ModelState.IsValid)
            {
                switch (command)
                {
                    case "Log In":
                        try
                        {
                            string MD5_Password = mainDAL.CalculateMD5Hash(data.Password);
                            User _user = db.Users.Where(w => w.Username.ToLower() == data.Username.ToLower() && w.Password == MD5_Password && !w.IsDeleted).FirstOrDefault();
                            if (_user != null)
                            {
                                string IsAdmin = _user.UserRole.RoleName == GlobalVariables.AdminRoleName ? "True" : "False";
                                GlobalVariables.StoreInCookie("NYCUser", IPGlobalProperties.GetIPGlobalProperties().DomainName, "Username", _user.Username, DateTime.Now.AddDays(2), false);
                                GlobalVariables.StoreInCookie("NYCUser", IPGlobalProperties.GetIPGlobalProperties().DomainName, "IsLogged", "True", DateTime.Now.AddDays(2), false);
                                GlobalVariables.StoreInCookie("NYCUser", IPGlobalProperties.GetIPGlobalProperties().DomainName, "IsAdmin", IsAdmin, DateTime.Now.AddDays(2), false);

                                if (MD5_Password == mainDAL.CalculateMD5Hash(GlobalVariables.InitPassword))
                                {
                                    return RedirectToAction("ChangePassword");
                                }
                                else
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Your username or password is not correct";
                                return View(data);
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = ex.InnerException != null ? "Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException.Message : "Message: " + ex.Message;
                            mainDAL.RecordInLogger("ERROR", "Login", message, "", "");
                            return View(data);
                        }
                    case "Sign Up":
                        try
                        {
                            if (db.Users.Any(w => w.Username.ToLower() == data.Username.ToLower()))
                            {
                                TempData["ErrorMessage"] = "This username is taken. Please choose other one";
                                return View(data);
                            }
                            else
                            {
                                User _user = new User
                                {
                                    Username = data.Username,
                                    Password = mainDAL.CalculateMD5Hash(data.Password),
                                    Role_ID = GlobalVariables.ViewerRoleID,
                                    IsDeleted = false
                                };
                                db.Users.Add(_user);
                                db.SaveChanges();
                                TempData["InfoMessage"] = "You are registered successfully";
                                return RedirectToAction("Login");
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = ex.InnerException != null ? "Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException.Message : "Message: " + ex.Message;
                            mainDAL.RecordInLogger("ERROR", "Sign Up", message, "", "");
                            TempData["ErrorMessage"] = "Some error happens and you are not registered successfully";
                            return View(data);
                        }
                }
            }

            return View(data);
        }

        //
        // POST: /AppUsers/LogOut
        public ActionResult LogOut()
        {
            GlobalVariables.RemoveCookie("NYCUser", "Username", IPGlobalProperties.GetIPGlobalProperties().DomainName);
            GlobalVariables.RemoveCookie("NYCUser", "IsAdmin", IPGlobalProperties.GetIPGlobalProperties().DomainName);
            GlobalVariables.RemoveCookie("NYCUser", "IsLogged", IPGlobalProperties.GetIPGlobalProperties().DomainName);
            return RedirectToAction("Login", "AppUsers");
        }

        // GET: /AppUsers/ResetPassword/5
        public ActionResult ResetPassword(int? User_ID)
        {
            if (!(GlobalVariables.GetFromCookie("NYCUser", "IsAdmin") == "True"))
                return RedirectToAction("Login", "AppUsers");
            if (User_ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUserInfo data = appUserDAL.GetAppUserInfoByID(User_ID.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: /AppUsers/ResetPassword/5
        [HttpPost]
        public ActionResult ResetPassword(AppUserInfo data)
        {
            try
            {
                User _user = db.Users.Where(w => w.User_ID == data.User_ID).FirstOrDefault();
                if (_user != null)
                {
                    _user.Password = mainDAL.CalculateMD5Hash(GlobalVariables.InitPassword);
                    db.Entry(_user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["InfoMessage"] = "The password has been successfully reset";
                    return RedirectToAction("Preview", "AppUsers");
                }
                else
                {
                    TempData["ErrorMessage"] = "Some error happens and the password is not reset. Contact the Admin.";
                    return View(data);
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? "Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException.Message : "Message: " + ex.Message;
                mainDAL.RecordInLogger("ERROR", "Reset Password", message, "", "");
                TempData["ErrorMessage"] = "Some error happens and the password is not reset. Contact the Admin.";
                return View(data);
            }
        }

    }
}