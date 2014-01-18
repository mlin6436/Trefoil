using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Trefoil.Library
{
    [DataContract]
    public class ProjectDto
    {
        [DataMember]
        public int? actualdurationminutes { get; set; }

        [DataMember]
        public DateTime? actualend { get; set; }

        [DataMember]
        public DateTime? actualstart { get; set; }

        [DataMember]
        public double? budget { get; set; }

        [DataMember]
        public int businessvalueid { get; set; }

        [DataMember]
        public string cancelledreason { get; set; }

        [DataMember]
        public int? contactid { get; set; }

        [DataMember]
        public int? createdby { get; set; }

        [DataMember]
        public DateTime createdon { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public bool isbilled { get; set; }

        [DataMember]
        public bool isdisabled { get; set; }

        [DataMember]
        public int? modifiedby { get; set; }

        [DataMember]
        public DateTime? modifiedon { get; set; }

        [DataMember]
        public int? organisationid { get; set; }

        [DataMember]
        public int? ownerid { get; set; }

        [DataMember]
        public int projectid { get; set; }

        [DataMember]
        public int prioritytypeid { get; set; }

        [DataMember]
        public string rejectedreason { get; set; }

        [DataMember]
        public int? scheduleddurationminutes { get; set; }

        [DataMember]
        public DateTime? scheduledend { get; set; }

        [DataMember]
        public DateTime? scheduledstart { get; set; }

        [DataMember]
        public int statetypeid { get; set; }

        [DataMember]
        public int statustypeid { get; set; }

        [DataMember]
        public string subject { get; set; }

        [DataMember]
        public double? totalcost { get; set; }

        [DataMember]
        public double? totalcharge { get; set; }
    }
}
