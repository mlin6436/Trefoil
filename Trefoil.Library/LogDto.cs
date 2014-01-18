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
    public class LogDto
    {
        [DataMember]
        public int? activityid { get; set; }

        [DataMember]
        public string applicationname { get; set; }

        [DataMember]
        public DateTime? createdon { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public bool isresolved { get; set; }

        [DataMember]
        public LevelCode levelcode { get; set; }

        [DataMember]
        public int logid { get; set; }

        [DataMember]
        public string subject { get; set; }
    }
}
