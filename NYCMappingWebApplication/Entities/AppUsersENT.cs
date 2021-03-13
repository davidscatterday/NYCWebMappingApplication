using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.Entities
{
    public class AppUserInfo
    {
        [Required(ErrorMessage = "Required field")]
        public int User_ID { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string Username { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Required field")]
        public int Role_ID { get; set; }
        public bool IsDeleted { get; set; }
        public List<SelectListItem> RoleNames = new List<SelectListItem>();
    }
    public class AppUserLoginInfo
    {
        [Required(ErrorMessage = "Required field")]
        public int User_ID { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^.*(?=.{6,})(?=.*\d)(?=.*[!,@,#,$,%,^,&,*,~,:,?,_,-,+]).*$", ErrorMessage = "Password must have at least 6 characters including 1 number and special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required field")]
        public int Role_ID { get; set; }
        public bool IsDeleted { get; set; }
        public List<SelectListItem> RoleNames = new List<SelectListItem>();
    }
    public class ChangePasswordInfo
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [RegularExpression(@"^.*(?=.{6,})(?=.*\d)(?=.*[!,@,#,$,%,^,&,*,~,:,?,_,-,+]).*$", ErrorMessage = "Password must have at least 6 characters including 1 number and special character")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}