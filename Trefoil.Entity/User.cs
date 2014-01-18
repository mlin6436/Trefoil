using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trefoil.Library;

namespace Trefoil.Entity
{
    public class User
    {
        [StringLength(50)]
        public string address1_city { get; set; }

        [StringLength(50)]
        public string address1_country { get; set; }

        [StringLength(50)]
        public string address1_county { get; set; }

        [StringLength(50)]
        public string address1_fax { get; set; }

        [StringLength(50)]
        public string address1_line1 { get; set; }

        [StringLength(50)]
        public string address1_line2 { get; set; }

        [StringLength(50)]
        public string address1_line3 { get; set; }

        [StringLength(100)]
        public string address1_name { get; set; }

        [StringLength(20)]
        public string address1_postalcode { get; set; }

        [StringLength(20)]
        public string address1_postofficebox { get; set; }

        [StringLength(50)]
        public string address1_telephone1 { get; set; }

        [StringLength(50)]
        public string address1_telephone2 { get; set; }

        [StringLength(50)]
        public string address1_telephone3 { get; set; }

        public BusinessFunction BusinessFunction { get; set; }

        public int? businessfunctionid { get; set; }

        public int? createdby { get; set; }

        public User CreatedByUser { get; set; }

        public DateTime? createdon { get; set; }

        [StringLength(100)]
        public string disabledreason { get; set; }

        [StringLength(100)]
        public string emailaddress { get; set; }

        [StringLength(50)]
        public string firstname { get; set; }

        [StringLength(160)]
        public string fullname { get; set; }

        [StringLength(100)]
        public string homephone { get; set; }

        [StringLength(500)]
        public string interest { get; set; }

        public bool isdisabled { get; set; }

        public bool isinternal { get; set; }

        [StringLength(100)]
        public string jobtitle { get; set; }

        [StringLength(50)]
        public string lastname { get; set; }

        [StringLength(50)]
        public string middlename { get; set; }

        [StringLength(50)]
        public string mobilephone { get; set; }

        public int? modifiedby { get; set; }

        public User ModifiedByUser { get; set; }

        public DateTime? modifiedon { get; set; }

        [StringLength(50)]
        public string nickname { get; set; }

        public Organisation Organisation { get; set; }

        public int? organisationid { get; set; }

        public int? parentsystemuserid { get; set; }

        public User ParentUser { get; set; }

        [StringLength(100)]
        public string password { get; set; }

        public ICollection<Project> Projects { get; set; }

        public RoleType RoleType { get; set; }

        public int roletypeid { get; set; }

        [Key]
        public int systemuserid { get; set; }

        [StringLength(100)]
        public string social_facebook { get; set; }

        [StringLength(100)]
        public string social_linkedin { get; set; }

        [StringLength(100)]
        public string social_twitter { get; set; }

        [StringLength(100)]
        public string social_vimeo { get; set; }

        [StringLength(100)]
        public string social_youtube { get; set; }

        public ICollection<Task> Tasks { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [StringLength(100)]
        public string website { get; set; }
    }
}
