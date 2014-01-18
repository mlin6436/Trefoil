using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trefoil.Web.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            rememberme = false;
        }

        [Display(Name="Verification")]
        public string captcha { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(100)]
        public string emailaddress { get; set; }

        [DisplayName("Full Name")]
        [StringLength(160)]
        public string fullname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100)]
        public string password { get; set; }

        [Display(Name = "Remember me?")]
        public bool rememberme { get; set; }

        [StringLength(100)]
        public string returnurl { get; set; }

        public int roletypeid { get; set; }

        [Key]
        public int systemuserid { get; set; }
    }
}