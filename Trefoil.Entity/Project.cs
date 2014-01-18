using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trefoil.Entity
{
    public class Project
    {
        public int? actualdurationminutes { get; set; }

        public DateTime? actualend { get; set; }

        public DateTime? actualstart { get; set; }

        public ICollection<User> AssignToUsers { get; set; }

        public double? budget { get; set; }

        public int businessvalueid { get; set; }

        public BusinessValue BusinessValue { get; set; }

        [StringLength(200)]
        public string cancelledreason { get; set; }

        public int? contactid { get; set; }

        public User Contact { get; set; }

        public int? createdby { get; set; }

        public User CreatedByUser { get; set; }

        public DateTime createdon { get; set; }

        [StringLength(2000)]
        public string description { get; set; }

        public bool isbilled { get; set; }

        public bool isdisabled { get; set; }

        public int? modifiedby { get; set; }

        public User ModifiedByUser { get; set; }

        public DateTime? modifiedon { get; set; }

        public ICollection<Note> Notes { get; set; }

        public Organisation Organisation { get; set; }

        public int? organisationid { get; set; }

        public int? ownerid { get; set; }

        public User Owner { get; set; }

        public int projectid { get; set; }

        public PriorityType PriorityType { get; set; }

        public int prioritytypeid { get; set; }

        [StringLength(200)]
        public string rejectedreason { get; set; }

        public int? scheduleddurationminutes { get; set; }

        public DateTime? scheduledend { get; set; }

        public DateTime? scheduledstart { get; set; }

        public StateType StateType { get; set; }

        public int statetypeid { get; set; }

        public StatusType StatusType { get; set; }

        public int statustypeid { get; set; }

        [Required]
        [StringLength(200)]
        public string subject { get; set; }

        public double? totalcost { get; set; }

        public double? totalcharge { get; set; }
    }
}
