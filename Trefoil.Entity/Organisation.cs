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
    public class Organisation
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

        public BusinessType BusinessType { get; set; }

        public int businesstypeid { get; set; }

        public BusinessStatus BusinessStatus { get; set; }

        public int businessstatusid { get; set; }

        public int? createdby { get; set; }

        public User CreatedByUser { get; set; }

        public DateTime? createdon { get; set; }

        [StringLength(100)]
        public string disabledreason { get; set; }

        public bool isdisabled { get; set; }

        public int? modifiedby { get; set; }

        public User ModifiedByUser { get; set; }

        public DateTime? modifiedon { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public ICollection<Note> Notes { get; set; }

        [Key]
        public int organisationid { get; set; }

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

        [StringLength(100)]
        public string website { get; set; }
    }
}
