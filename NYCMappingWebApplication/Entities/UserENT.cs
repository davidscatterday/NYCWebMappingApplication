using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.Entities
{
    public class UserTabs
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string TabIDs { get; set; }
        public string TabNames { get; set; }
    }
    public class AddUserTabs
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string TabIDs { get; set; }
    }
}