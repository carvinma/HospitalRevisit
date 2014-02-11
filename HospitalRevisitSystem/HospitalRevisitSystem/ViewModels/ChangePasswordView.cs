using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HospitalRevisitSystem.ViewModels
{
    public class ChangePasswordView
    {

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string New_Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        public string Confirm_New_Password { get; set; }
    }
    public class LogOnModel
    {
        [Required]
        [Display(Name = "会员账户")]
        public string Staff_Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }


    }

}