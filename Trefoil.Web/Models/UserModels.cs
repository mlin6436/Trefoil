using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trefoil.Helper;

namespace Trefoil.Web.Models
{
    public class UserModel
    {
        public UserModel()
        {
            businessfunctionid = 1;
            organisationid = 1;
        }

        [DisplayName("City")]
        [StringLength(50)]
        public string address1_city { get; set; }

        [DisplayName("Country")]
        [StringLength(50)]
        public string address1_country { get; set; }

        [DisplayName("County")]
        [StringLength(50)]
        public string address1_county { get; set; }

        [DisplayName("Fax")]
        [StringLength(50)]
        public string address1_fax { get; set; }

        [DisplayName("Address Line 1")]
        [StringLength(50)]
        public string address1_line1 { get; set; }

        [DisplayName("Address Line 2")]
        [StringLength(50)]
        public string address1_line2 { get; set; }

        [DisplayName("Address Line 3")]
        [StringLength(50)]
        public string address1_line3 { get; set; }

        [DisplayName("Address Name")]
        [StringLength(100)]
        public string address1_name { get; set; }

        [DisplayName("Post Code")]
        [StringLength(20)]
        public string address1_postalcode { get; set; }

        [DisplayName("Post Office Box")]
        [StringLength(20)]
        public string address1_postofficebox { get; set; }

        [DisplayName("Telephone 1")]
        [StringLength(50)]
        public string address1_telephone1 { get; set; }

        [DisplayName("Telephone 2")]
        [StringLength(50)]
        public string address1_telephone2 { get; set; }

        [DisplayName("Telephone 3")]
        [StringLength(50)]
        public string address1_telephone3 { get; set; }

        [DisplayName("Business Function")]
        public int businessfunctionid { get; set; }

        public DateTime? createdon { get; set; }

        [StringLength(100)]
        public string disabledreason { get; set; }

        [DisplayName("Email Address")]
        [StringLength(100)]
        public string emailaddress { get; set; }

        [DisplayName("First Name")]
        [StringLength(50)]
        public string firstname { get; set; }

        [DisplayName("Full Name")]
        [StringLength(160)]
        public string fullname { get; set; }

        [DisplayName("Home Telephone")]
        [StringLength(100)]
        public string homephone { get; set; }

        [DisplayName("Interests")]
        [StringLength(500)]
        public string interest { get; set; }

        public bool isdisabled { get; set; }

        [DisplayName("Internal User")]
        public bool isinternal { get; set; }

        [DisplayName("Job Title")]
        [StringLength(100)]
        public string jobtitle { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50)]
        public string lastname { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(50)]
        public string middlename { get; set; }

        [DisplayName("Mobile")]
        [StringLength(50)]
        public string mobilephone { get; set; }

        [DisplayName("Nickname")]
        [StringLength(50)]
        public string nickname { get; set; }

        public int? organisationid { get; set; }

        public int? parentsystemuserid { get; set; }

        public int roletypeid { get; set; }

        [Key]
        public int systemuserid { get; set; }

        [DisplayName("Facebook")]
        [StringLength(100)]
        public string social_facebook { get; set; }

        [DisplayName("LinkedIn")]
        [StringLength(100)]
        public string social_linkedin { get; set; }

        [DisplayName("Twitter")]
        [StringLength(100)]
        public string social_twitter { get; set; }

        [DisplayName("Vimeo")]
        [StringLength(100)]
        public string social_vimeo { get; set; }

        [DisplayName("Youtube")]
        [StringLength(100)]
        public string social_youtube { get; set; }

        [DisplayName("Title")]
        [StringLength(100)]
        public string title { get; set; }

        [DisplayName("Website")]
        [StringLength(100)]
        public string website { get; set; }



        public IEnumerable<SelectListItem> BusinessFuntionCode { get; set; }

        public string businessfunctionname { get; set; }
    }

    public class UserOrganisationModel
    {
        public UserModel UserModel { get; set; }
        public OrganisationModel OrganisationModel { get; set; }
    }
}