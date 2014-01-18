using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Trefoil.Helper;

namespace Trefoil.Library
{
    [DataContract]
    public class TaskDto
    {
        [DataMember]
        public int? actualdurationminutes { get; set; }

        [DataMember]
        public DateTime? actualend { get; set; }

        [DataMember]
        public DateTime? actualstart { get; set; }

        [DataMember]
        public int? createdby { get; set; }

        [DataMember]
        public DateTime createdon { get; set; }

        [DataMember]
        public string description { get; set; }

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
        public int prioritytypeid { get; set; }

        [DataMember]
        public int? projectid { get; set; }

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
        public int taskid { get; set; }
    }
}
